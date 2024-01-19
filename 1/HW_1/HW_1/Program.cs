using System.Reflection;

namespace HW_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var grandFather = new FamilyMember() { mother = null, father = null, name = "Дедушка", gender = Gender.Male };
            var grandMother = new FamilyMember() { mother = null, father = null, name = "Бабушка", gender = Gender.Female };
            var father = new FamilyMember() { mother = grandMother, father = grandFather, name = "Папа", gender = Gender.Male };
            var uncle1 = new FamilyMember() { mother = grandMother, father = grandFather, name = "Дядя1", gender = Gender.Male };
            var uncle2 = new FamilyMember() { mother = grandMother, father = grandFather, name = "Дядя2", gender = Gender.Male };
            var aunt1 = new FamilyMember() { mother = grandMother, father = grandFather, name = "Тетя1", gender = Gender.Female };
            var aunt2 = new FamilyMember() { mother = grandMother, father = grandFather, name = "Тетя2", gender = Gender.Female };
            grandFather.children = new FamilyMember[] { father, uncle1, uncle2, aunt1, aunt2 };
            grandMother.children = new FamilyMember[] { father, uncle1, uncle2, aunt1, aunt2 };
            var mother = new FamilyMember() { mother = null, father = null, name = "Мама", gender = Gender.Female };
            var mother2 = new FamilyMember() { mother = null, father = null, name = "Мама2", gender = Gender.Female };
            var son = new FamilyMember() { mother = mother, father = father, name = "Сын", gender = Gender.Male };
            FamilyMember son2 = new FamilyMember() { father = father, mother = mother2, name = "Сын2", gender = Gender.Male };
            mother.children = new FamilyMember[] { son };
            mother2.children = new FamilyMember[] { son2 };
            father.children = new FamilyMember[] { son, son2 };

            father.betrothed = mother;
            mother.betrothed = father;


            Console.WriteLine(father.CloseRelativesInfo());
        }
    }
}
