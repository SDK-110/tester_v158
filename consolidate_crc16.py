"""
Consolidate duplicate CRC16 and tan_modbus implementations across the project.
Replaces function bodies with calls to the unified ModbusCrc16 class.
"""
import os
import re

PROJECT_DIR = r"F:\tester_2960721\tester_v156\testapp"

files_modified = 0
crc16_replaced = 0
tan_modbus_replaced = 0
my_crc16_replaced = 0
my_tan_modbus_replaced = 0
other_crc_replaced = 0
errors = []

def find_cs_files(directory):
    cs_files = []
    for root, dirs, files in os.walk(directory):
        dirs[:] = [d for d in dirs if d not in ('obj', 'bin', '.vs')]
        for f in files:
            if f.endswith('.cs'):
                cs_files.append(os.path.join(root, f))
    return cs_files

def replace_crc16_body(content):
    """Replace crc16 function body. Uses 'return (crc); }' as anchor since
    the function body has nested braces but always ends with this pattern."""
    count = 0
    
    # Match: [modifiers] UInt16 crc16(Byte[] ptr) { ... return (crc); }
    # The [\s\S]*? matches any character (including newlines) non-greedily
    # We anchor on 'return (crc);' followed by closing brace
    pattern = r'((?:private|public|protected|internal)?\s*(?:static\s+)?UInt16\s+crc16\s*\(\s*Byte\[\]\s+ptr\s*\)\s*\{)[\s\S]*?return\s*\(crc\)\s*;\s*\}'
    
    def replacer(m):
        nonlocal count
        count += 1
        return m.group(1) + ' return ModbusCrc16.Compute(ptr); }'
    
    content = re.sub(pattern, replacer, content)
    return content, count

def replace_tan_modbus_body(content):
    """Replace tan_modbus function body. Return variable may be 'z', 'result', etc."""
    count = 0
    
    # Match: Byte[] tan_modbus(Byte[] data) { ... return <varname>; }
    # The return variable name varies (z, result, etc.) so we use \w+
    pattern = r'((?:private|public|protected|internal)?\s*(?:static\s+)?Byte\[\]\s+tan_modbus\s*\(\s*Byte\[\]\s+data\s*\)\s*\{)[\s\S]*?return\s+\w+\s*;\s*\}'
    
    def replacer(m):
        nonlocal count
        count += 1
        return m.group(1) + ' return ModbusCrc16.AppendCrc(data); }'
    
    content = re.sub(pattern, replacer, content)
    return content, count

def replace_my_crc16_body(content):
    """Replace my_crc16 function body."""
    count = 0
    
    pattern = r'((?:private|public|protected|internal)?\s*(?:static\s+)?UInt16\s+my_crc16\s*\(\s*Byte\[\]\s+ptr\s*\)\s*\{)[\s\S]*?return\s*\(crc\)\s*;\s*\}'
    
    def replacer(m):
        nonlocal count
        count += 1
        return m.group(1) + ' return ModbusCrc16.Compute(ptr); }'
    
    content = re.sub(pattern, replacer, content)
    return content, count

def replace_my_tan_modbus_body(content):
    """Replace my_tan_modbus function body. Return variable may vary."""
    count = 0
    
    pattern = r'((?:private|public|protected|internal)?\s*(?:static\s+)?Byte\[\]\s+my_tan_modbus\s*\(\s*Byte\[\]\s+data\s*\)\s*\{)[\s\S]*?return\s+\w+\s*;\s*\}'
    
    def replacer(m):
        nonlocal count
        count += 1
        return m.group(1) + ' return ModbusCrc16.AppendCrc(data); }'
    
    content = re.sub(pattern, replacer, content)
    return content, count

def process_file(filepath):
    global files_modified, crc16_replaced, tan_modbus_replaced, my_crc16_replaced, my_tan_modbus_replaced
    
    try:
        with open(filepath, 'rb') as f:
            raw = f.read()
        if raw[:3] == b'\xef\xbb\xbf':
            content = raw.decode('utf-8-sig')
        else:
            try:
                content = raw.decode('utf-8')
            except:
                content = raw.decode('gbk')
    except Exception as e:
        errors.append("Cannot read %s: %s" % (filepath, e))
        return
    
    original_content = content
    
    content, c1 = replace_crc16_body(content)
    crc16_replaced += c1
    
    content, c2 = replace_tan_modbus_body(content)
    tan_modbus_replaced += c2
    
    content, c3 = replace_my_crc16_body(content)
    my_crc16_replaced += c3
    
    content, c4 = replace_my_tan_modbus_body(content)
    my_tan_modbus_replaced += c4
    
    total = c1 + c2 + c3 + c4
    
    if total > 0 and content != original_content:
        try:
            # Preserve BOM
            with open(filepath, 'w', encoding='utf-8-sig') as f:
                f.write(content)
            files_modified += 1
            rel_path = os.path.relpath(filepath, PROJECT_DIR)
            print("  Modified: %s (crc16:%d, tan_modbus:%d, my_crc16:%d, my_tan_modbus:%d)" % (rel_path, c1, c2, c3, c4))
        except Exception as e:
            errors.append("Cannot write %s: %s" % (filepath, e))

def main():
    print("Scanning for .cs files in %s..." % PROJECT_DIR)
    cs_files = find_cs_files(PROJECT_DIR)
    print("Found %d .cs files" % len(cs_files))
    print()
    
    for filepath in cs_files:
        process_file(filepath)
    
    print()
    print("=" * 60)
    print("SUMMARY:")
    print("  Files modified:           %d" % files_modified)
    print("  crc16() replaced:         %d" % crc16_replaced)
    print("  tan_modbus() replaced:    %d" % tan_modbus_replaced)
    print("  my_crc16() replaced:      %d" % my_crc16_replaced)
    print("  my_tan_modbus() replaced: %d" % my_tan_modbus_replaced)
    print("  Total replacements:       %d" % (crc16_replaced + tan_modbus_replaced + my_crc16_replaced + my_tan_modbus_replaced))
    print("  Errors:                   %d" % len(errors))
    
    if errors:
        print()
        print("ERRORS:")
        for e in errors:
            print("  %s" % e)

if __name__ == '__main__':
    main()
