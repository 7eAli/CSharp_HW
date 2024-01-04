using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_1
{
    enum Gender
    {
        Male, Female
    }

    class FamilyMember
    {
        public string name { get; set; }
        public Gender gender { get; set; }
        public FamilyMember[] children { get; set; }
        public FamilyMember mother { get; set; }
        public FamilyMember father { get; set; }

        public FamilyMember betrothed { get; set; }

        public FamilyMember() { }

        public FamilyMember(string name, Gender gender, FamilyMember mother, FamilyMember father, FamilyMember betrothed, params FamilyMember[]? familyMembers)
        {
            this.name = name;
            this.gender = gender;
            this.children = familyMembers;
            this.betrothed = betrothed;
            this.mother = mother;
            this.father = father;
        }

        public void MothersInFamily()
        {
            FamilyMember adult = this;

            if (adult.mother != null)
            {
                adult = adult.children.Length > 0 && adult.children[0].mother != null ? adult.children[0].mother : this;
            }

            while (adult.mother != null)
                adult = adult.mother;

            if (adult.gender == Gender.Female)
                Console.Write($"{adult.name} -> ");

            bool femaleChild = true;
            while (femaleChild)
            {
                femaleChild = false;
                foreach (FamilyMember child in adult.children)
                {
                    if (child.gender == Gender.Female)
                    {
                        adult = child;
                        femaleChild = true;
                        break;
                    }
                }
            }
        }

        public void PrintFamily()
        {
            FamilyMember secondMember = null;
            if (this.children != null)
            {
                secondMember = this.gender == Gender.Male ? this.children[0].mother : this.children[0].father;
            }

            if (secondMember != null)
                PrintFamily(this, secondMember);
            else
                PrintFamily(this);
        }

        private void PrintFamily(params FamilyMember[] familyMembers)
        {
            List<FamilyMember> children = new List<FamilyMember>();

            foreach (FamilyMember familyMember in familyMembers)
                Console.Write($"{familyMember.name} ");
            Console.WriteLine();

            foreach (FamilyMember familyMember in familyMembers)
            {
                if (familyMember.children != null)
                {
                    foreach (FamilyMember child in familyMember.children)
                    {
                        if (child.children != null)
                        {
                            foreach (FamilyMember child2 in child.children)
                            {
                                FamilyMember second = child.gender == Gender.Male ? child2.mother : child2.father;
                                if (!children.Contains(second) && second != null)
                                    children.Add(second);
                            }
                        }
                        if (!children.Contains(child))
                            children.Add(child);
                    }
                }
            }
            if (children.Count > 0)
                PrintFamily(children.ToArray());
        }

        public string CloseRelativesInfo()
        {
            StringBuilder sb = new StringBuilder();
            List<FamilyMember> siblings = new List<FamilyMember>();
            if (this.mother.children != null)
                foreach (FamilyMember sibling in this.mother.children)
                {
                    if (sibling != this)
                        siblings.Add(sibling);
                }
            if (this.father.children != null)
                foreach (FamilyMember sibling in this.father.children)
                {
                    if (sibling != this && !siblings.Contains(sibling))
                        siblings.Add(sibling);
                }

            List<FamilyMember> married = new List<FamilyMember>();

            if (this.children != null)
                foreach (FamilyMember child in this.children)
                {
                    if (this.gender == Gender.Male && !married.Contains(child.mother))
                        married.Add(child.mother);
                    else if (this.gender == Gender.Female && !married.Contains(child.father))
                        married.Add(child.father);
                }
            sb.Append("Братья и сестры:\n");
            foreach (FamilyMember sibling in siblings)
            {
                if (sibling.gender == Gender.Male) 
                {
                    sb.Append($"Брат - {sibling.name}\n");
                } 
                else if (sibling.gender == Gender.Female)
                {
                    sb.Append($"Сестра - {sibling.name}\n");
                }
            }
            sb.Append("\n");
            if (this.gender == Gender.Male)
            {
                sb.Append("Жены:\n");
                foreach (FamilyMember betrothed in married)
                {
                    if (betrothed == this.betrothed)
                        sb.Append($"Жена - {betrothed.name}\n");
                    else
                        sb.Append($"Бывшая жена - {betrothed.name}\n");
                }
            }
            else if (this.gender == Gender.Female)
                {
                    sb.Append("Мужья:\n");
                    foreach (FamilyMember betrothed in married)
                    {                        
                        if (betrothed == this.betrothed)
                            sb.Append($"Муж - {betrothed.name}\n");
                        else
                            sb.Append($"Бывший муж - {betrothed.name}\n");                        
                    }
                }

            return sb.ToString();
        }

        public bool CheckIfSibling(FamilyMember sibling)
        {
            bool result = false;
            if (this.father.children.Contains(sibling)) result = true;
            if (this.mother.children.Contains(sibling)) result = true;
            return result;
        }

        public bool CheckIfBetrothed(FamilyMember betrothed)
        {
            bool result = false;
            foreach (FamilyMember child in betrothed.children)
            {
                if (this.children.Contains(child)) result = true;
            }
            return result;
        }
    }
}
