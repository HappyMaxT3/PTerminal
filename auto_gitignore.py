import os

ignore_patterns = [
    '__pycache__', '*.pyc', '*.pyo',

    '.DS_Store',

    'Thumbs.db', 'ehthumbs.db',

    'node_modules/',

    '*.log', '*.tmp', '*.temp', '*.bak',

    '.vscode/', '.idea/', '*.suo', '*.user', '*.userosscache', '*.sln.docstates',

    '[Bb]in/', '[Oo]bj/', 'dist/', 'build/',

    '[Dd]ebug/', '[Rr]elease/', 'x64/', 'x86/', '.vs/', 'artifacts/', 'mono_crash.*',

    '[Pp]ackages/', '.paket/',

    '.gradle/', '.idea/', 'local.properties', '.android/',

    '*.swp', '*.tmp_proj', '*~',
    "bin/",
    "obj/",
    ".platforms/",
    ".platforms/android/",
    ".platforms/ios/",
    ".platforms/maccatalyst/",
    ".platforms/windows/",
    "*.essentials/",
    "**/*.maui.csproj/",
    "*.xcworkspace/",
    "*.xcuserdata/",
    "*.xcodeproj/",
    "*.pbxproj/",
    "*.xcuserstate/",
    "*.apk",
    "*.aab",
    "*.dex",
    "*.apk.mdbg",
    "*.ipa",
    "*.dSYM/",
    "Resources/**/*.png",
    "Resources/**/*.jpg",
    "Resources/**/*.svg",
    "diagnostics.txt",
    "msbuild.binlog",
    ".idea/",
    "*.keystore",
    ".gradle/",
    ".idea/",
    ".cxx/",
    ".externalNativeBuild/",
    ".vs/",
    "gradle/",
    "local.properties",
    "app/build/",
    "**/build/",
    "**/*.apk",
    "**/*.aar",
    "**/*.iml",
    "captures/",
    ".settings/",
    "*.class",
    "*.jar",
    "*.war",
    "*.ear",
    "*.log",
    "*.jmx",
    "*.jfr",
    "hs_err_pid*",
    "target/",
    "*.pom",
    "build/",
    ".gradletasknamecache",
    ".gradle/buildOutputCleanup/",
    ".classpath",   
    ".project",     
    ".settings/",
    "*.class",   
    "*.jar",    
    "*.war",    
    "*.ear",   
    "target/",  
    "*.log",   
    "*.jfr",    
    "hs_err_pid*", 
    "*.java.backup",
    "*.apk",   
    "*.aab",    
    "*.dex",    
    "build/",    
    "app/build/", 
    ".gradle/",    
    ".idea/",     
    "*.iml",     
    "captures/",  
    ".cxx/",     
    ".externalNativeBuild/",
    "local.properties", 
    "*.keystore",  
    "*.hprof",      
    ".android/",    
    "gradle/wrapper/gradle-wrapper.properties", 
    "gradle/",      
    "app/build.gradle", 
    ".maui/",
    "MauiApp.csproj",
    "MauiApp.sln",
    "MauiApp/bin/",
    "MauiApp/obj/",
    "MauiApp/Properties/",
    "MauiApp/Resources/",
    "**/bin/",
    "**/obj/",
    "**/Properties/",
    "**/Resources/",
    "*.maui/",
    "*.maui.*",
    "**/*.maui/",
    "**/*.maui.*",
    "*.xcworkspace/",
    "*.xcuserdata/",
    "*.xcodeproj/",
    "*.pbxproj/",
    "*.xcuserstate/",
    "*.xcuserdatad/",
    "DerivedData/",
    "build/",
    "*.dSYM/",
    "*.hmap",
    "xcshareddata/",
    "xcuserdata/",
    "*.xcscheme",
    "*.xccheckout",
    "*.xcscheme",
    "*.xcsnap",
    "*.xcschemes",
    "*.xcassets",
    "*.xctest",
    "<JavaSdkPath>/lib/modules/",
    "<JavaSdkPath>/lib/tools.jar",
    "<JavaSdkPath>/lib/ant-launcher.jar",
    "<JavaSdkPath>/lib/ant.jar",
    "<JavaSdkPath>/lib/charsets.jar",
    "<JavaSdkPath>/lib/dt.jar",
    "<JavaSdkPath>/lib/jconsole.jar",
    "<JavaSdkPath>/lib/jdk.jshell.jar",
    "<JavaSdkPath>/lib/jdk.management.agent.jar",
    "<JavaSdkPath>/lib/jdk.sctp.jar",
    "<JavaSdkPath>/lib/jdk.unsupported.jar",
    "<JavaSdkPath>/lib/jps-launcher.jar",
    "<JavaSdkPath>/lib/pack200.jar",
    "<JavaSdkPath>/lib/plugin.jar",
    "<JavaSdkPath>/lib/resources.jar",
    "<JavaSdkPath>/lib/tools.jar",
    "<JavaSdkPath>/lib/zipfs.jar",
    "javasdk/*",
    "javasdk/lib/modules",
    "javasdk/lib/*",
    "javasdk/bin/*",
    "javasdk/include/*",
    "javasdk/jre/*",
    "javasdk/jdk/*",
    "androidsdk/*",
    "androidsdk/platforms/*",
    "androidsdk/build-tools/*",
    "androidsdk/ndk/*",
    "androidsdk/tools/*",
    "androidsdk/system-images/*"
]



def should_ignore(path):
    for pattern in ignore_patterns:
        if os.path.isdir(path) and pattern.endswith('/'):
            if pattern[:-1] in path.split(os.sep):
                return True
        if os.path.isfile(path) and path.endswith(pattern):
            return True
    return False

def create_gitignore(directory='.'):
    gitignore_path = os.path.join(directory, '.gitignore')

    ignore_entries = set()

    for root, dirs, files in os.walk(directory):
        for d in dirs:
            dir_path = os.path.join(root, d)
            if should_ignore(dir_path):
                relative_path = os.path.relpath(dir_path, directory)
                ignore_entries.add(f'/{relative_path}/')

        for f in files:
            file_path = os.path.join(root, f)
            if should_ignore(file_path):
                relative_path = os.path.relpath(file_path, directory)
                ignore_entries.add(f'/{relative_path}')

    if ignore_entries:
        with open(gitignore_path, 'w') as f:
            for entry in sorted(ignore_entries):
                f.write(entry + '\n')
        print(f'file {gitignore_path} completed')
    else:
        print('there is no files to ignore')

if __name__ == '__main__':
    create_gitignore()
