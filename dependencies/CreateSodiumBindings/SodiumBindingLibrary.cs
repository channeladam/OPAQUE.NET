using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;

namespace CreateSodiumBindings
{
    public class SodiumBindingLibrary : ILibrary
    {
        public override void Postprocess(Driver driver, ASTContext ctx)
        {
        }

        public override void Preprocess(Driver driver, ASTContext ctx)
        {
        }

        public override void Setup(Driver driver)
        {
            var options = driver.Options;
            options.GeneratorKind = GeneratorKind.CSharp;

            var module = options.AddModule("Opaque.Net.Sodium");
            module.LibraryDirs.Add(@"/home/adam/.nuget/packages/libsodium/1.0.18/runtimes/linux-x64/native/");
            module.Libraries.Add("libsodium.so");
            module.IncludeDirs.Add(@"/home/adam/repos/ChannelAdam/OPAQUE.NET/dependencies/sodium/libsodium-stable/src/libsodium/include/sodium");
            module.Headers.Add("core.h");
            module.Headers.Add("utils.h");
            // module.Headers.Add("crypto_scalarmult_curve25519.h");
            module.Headers.Add("crypto_core_ristretto255.h");
            module.Headers.Add("crypto_scalarmult_ristretto255.h");
            module.Headers.Add("version.h");
        }

        public override void SetupPasses(Driver driver)
        {
        }
    }
}