# Domeitc CPV 599 开机/关机电压找寻法测试设计

## 概述

在 `Domeitc_CPV_599_project.cs` 中新增两个测试函数，使用**找寻法**（扫描逼近）检测车载冰箱的**开机电压**和**关机电压**，验证是否符合客户规格。

## 相关文件

| 文件 | 角色 |
|---|---|
| `testapp/test_cases/Domeitc_CPV_599_project.cs` | **修改** — 新增两个测试函数并注册 |
| `testapp/mylib/TH6300_PowerSupply.cs` | 电源驱动，无需修改 |

## 客户规格

| 测试项 | 规格 |
|--------|------|
| 开机电压 | 10.7V ± 0.3 → [10.4V, 11.0V] |
| 关机电压 | 9.7V ± 0.3 → [9.4V, 10.0V] |

---

## 1. power_on_test_search — 开机电压找寻法

### 参数格式（d 参数）

```
"standby_limit,start_current,confirm_current,sample_count,sample_interval_ms,confirm_sec,range_min,range_max,wire_loss"
```

字段说明：

| 参数 | 默认值 | 说明 |
|------|--------|------|
| standby_limit | 0.15 | 待机电流上限，超过即判定启动 |
| start_current | 0.15 | 启动检测阈值 |
| confirm_current | 2.0 | 确认压缩机完全工作的电流 |
| sample_count | 5 | 每电压点采样次数 |
| sample_interval_ms | 1000 | 采样间隔(ms) |
| confirm_sec | 20 | 启动后确认阶段的超时秒数 |
| range_min | 10.4 | 规格下限 |
| range_max | 11.0 | 规格上限 |
| wire_loss | 0.05 | 线损补偿 |

### 扫描参数

| 项目 | 值 |
|------|-----|
| 起始电压 | range_min - 0.1 = 10.3V |
| 终点电压 | range_max = 11.0V |
| 步进 | (终点 - 起始) / 7 = 0.1V |
| 电压点数 | 8 个 (10.3, 10.4, ..., 11.0) |

### 算法

```
for 每个电压点 v = 起始 到 终点, 步进 0.1V:
  set_vol_cur(v + wire_loss, 10.0)
  Thread.Sleep(100)

  for s = 0 .. sample_count-1:
    Thread.Sleep(sample_interval_ms)     // 1s
    cur = getCurrent()

    if v == 起始 && cur > start_current:
      // 第一点(10.3V, 规格范围外)就启动 → NG
      return "fail"  (out c = "fail;start_outside_range")

    if cur > start_current:
      goto CONFIRM_PHASE  // 找到启动点

// 到终点都没超过阈值 → NG
return "fail"  (out c = "fail;no_start")

CONFIRM_PHASE:
  保持当前电压 v

  for i = 0 .. confirm_sec-1:     // 20s
    Thread.Sleep(1000)
    cur = getCurrent()

    if cur < standby_limit:
      // 启动后电流又回落 → 不稳定
      return "fail"  (out c = "fail;current_dropped")

    if i >= 2:
      last_3_avg = 最近3次电流的滑动平均值
      if last_3_avg > confirm_current && v ∈ [range_min, range_max]:
        c = "pass"
        return "pass"

  // 20秒内都没达到 confirm_current
  return "fail"  (out c = "fail;current_not_enough")
```

### 判据总结

| 条件 | 结果 |
|------|------|
| 在 10.3V 时电流 > 0.15A | NG — 规格范围外启动 |
| 一直升到 11.0V 电流始终 ≤ 0.15A | NG — 在规格内不启动 |
| 找到启动点后 20s 内电流达不到 2A | NG — 启动但未进入正常工作 |
| 确认阶段电流回落到 ≤ 0.15A | NG — 启动不稳定 |
| 启动点电压 ∈ [10.4, 11.0] 且连续3次电流 > 2A | **PASS** |

---

## 2. power_off_test_search — 关机电压找寻法

### 参数格式（d 参数）

```
"boost_v,boost_current,start_check_current,sample_count,sample_interval_ms,shutdown_current,drop_step_v,range_min,range_max,wire_loss"
```

字段说明：

| 参数 | 默认值 | 说明 |
|------|--------|------|
| boost_v | 12.0 | 用于启动压缩机的电压 |
| boost_current | 3.0 | 确认压缩机启动的电流阈值 |
| start_check_current | 3.0 | 10.1V 预检时电流不能低于此值 |
| sample_count | 5 | 每电压点采样次数 |
| sample_interval_ms | 1000 | 采样间隔(ms) |
| shutdown_current | 0.15 | 判定关断的电流阈值 |
| drop_step_v | 0.1 | 降压步进 |
| range_min | 9.4 | 规格下限 |
| range_max | 10.0 | 规格上限 |
| wire_loss | 0.05 | 线损补偿 |

### 算法

```
// Phase A: 启动压缩机
set_vol_cur(boost_v + wire_loss, 10.0)    // 12.05V
set_on_off(1)

loop:
  读电流 cur
  if cur > boost_current (3A):
    running_current ≈ cur
    break

// Phase B: 降到检测起点 10.1V, 确认电流正常
start_v = range_max + 0.1                  // 10.1V
set_vol_cur(start_v + wire_loss, 10.0)

for s = 0 .. sample_count-1:               // 5次, 每秒一次
  Thread.Sleep(sample_interval_ms)
  cur = getCurrent()
  if cur < start_check_current:            // 电流掉了 → 无法维持
    return "fail" (out c = "fail;current_dropped_at_start")

// Phase C: 降压扫描
for v = start_v; v >= range_min; v -= drop_step_v:
  set_vol_cur(v + wire_loss, 10.0)

  for s = 0 .. sample_count-1:             // 5秒, 每秒一次
    Thread.Sleep(sample_interval_ms)
    cur = getCurrent()

    if cur < shutdown_current (0.15A):     // 找到关机点
      if v ∈ [range_min, range_max]:       // 在规格内?
        c = "pass"
        return "pass"
      else:
        c = "fail"
        return "fail"

// 降到 range_min (9.4V) 仍 ≥ 0.15A
return "fail" (out c = "fail;no_shutdown")
```

### 判据总结

| 条件 | 结果 |
|------|------|
| 10.1V 预检时电流 < 3A | NG — 冰箱无法在 10.1V 维持运行 |
| 关机点电压 ∈ [9.4V, 10.0V] | **PASS** |
| 关机点电压 < 9.4V | NG — 低于规格下限才关机 |
| 关机点电压 > 10.0V | NG — 在规格上限以上就关机 |
| 降到 9.4V 仍 ≥ 0.15A | NG — 在规格内不关断 |

---

## 3. 注册方式

在 `add_func_to_libs()` 中增加：
```csharp
tc.funcs.Add(id + "power_on_test_search", power_on_test_search);
tc.funcs.Add(id + "power_off_test_search", power_off_test_search);
```

---

## 边界情况处理

| 场景 | 处理 |
|------|------|
| 串口通信失败 | getCurrent() 返回负值 → 跳过该次读数，累计连续失败 >3 次 → 返回 fail |
| 电压点不整除 | 步进采用浮点累加，最后一档强制到终点值 |
| 电流抖动 | 开机确认用 **连续 3 次** 的滑动平均值，避免单点毛刺误判 |
| 规格边界含等号 | `v >= range_min && v <= range_max`，边界上判定为 PASS |
