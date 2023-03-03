import os
import glob
import zipfile

release_folder = "./Release/"

if not os.path.exists(release_folder):
    os.mkdir(release_folder)
    
for file in glob.glob(release_folder + "**"):
    os.remove(file)
    
os.system(f'dotnet publish -r win-x64 -o {release_folder} -c Release /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:DebugType=None /p:DebugSymbols=false --self-contained false')

os.startfile(os.path.realpath(release_folder))
    