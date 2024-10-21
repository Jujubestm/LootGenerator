all:
    dotnet build LootGenerator.csproj --configuration Release --verbosity q --self-contained
    ln -s ./bin/Release/net8.0/linux-x64/LootGenerator ~/LootGenerator.exe

clean:
    rm ./bin -dr
    rm ~/LootGenerator.exe
