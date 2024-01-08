using System;

namespace CSharpBasicApp
{
    class Parent
    {
        public int PVarPublic;
        private int PVarPrivate;
        protected int PVarProtected;
        public Parent()
        {

        }
        public Parent(int PVarPublic, int PVarPrivate, int PVarProtected)
        {
            this.PVarPublic = PVarPublic;
            this.PVarPrivate = PVarPrivate;
            this.PVarProtected = PVarProtected;
        }
    }
    class Child : Parent
    {
        public int CVarPublic;
        public Child() : base(0, 0, 0)
        {

        }
        public Child(int PVarPublic, int PVarPrivate, int PVarProtected, int CvarPublic) : base(PVarPublic, PVarPrivate, PVarProtected)
        {
            this.CVarPublic = CvarPublic;
        }
        public void CFuncPublic()
        {
            this.PVarPublic = 100;
            // private member of Parent class are not accessible in Child class
            // this.PVarPrivate = 200;
            this.PVarProtected = 300;

            Child c = new Child();
            c.PVarPublic = 100;
            // c.PVarPrivate = 200;
            c.PVarProtected = 200;

            Parent p = new Parent();
            p.PVarPublic = 100;
            // p.PvarPrivate = 200;
            // protected member is not accessible from Child class if you are accessing it Parent class refrence variable
            //p.PVarProtected = 300;
        }
    }
    internal class MyClassInheritance
    {
        static void Main()
        {

        }
    }
}
