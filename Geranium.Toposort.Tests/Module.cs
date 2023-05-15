namespace Geranium.Toposorts.Tests
{
    internal class BaseModule : ToposortType
    {
    }

    class Module1 : BaseModule
    {
        protected override void SetDependencies()
        {
            base.SetDependencies();
        }
    }

    class Module2 : BaseModule
    {
        protected override void SetDependencies()
        {
            this.DependsOn<Module3>();
        }
    }

    class Module3 : BaseModule
    {
        protected override void SetDependencies()
        {
            this.DependsOn<Module1>();
        }
    }

    class ModuleWeight1 : BaseModule
    {
        public override int Weight => -15;
    }

    class ModuleWeight2 : BaseModule
    {
        public override int Weight => 76;
    }

    class ModuleWeight3 : BaseModule
    {
        public override int Weight => 2;
    }
}