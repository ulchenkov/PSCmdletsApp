using System;
using System.Collections.Generic;
using System.Text;

namespace ETL
{
    public partial class Person
    {
        const int MINIMUM_AGE = 16;
        const int MAXIMUM_AGE = 76;

        static readonly string[] MaleFirstNames = { "James", "John", "Robert", "Michael", "William", "David",
             "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Donald",
             "Mark", "Paul", "Steven", "Andrew", "Kenneth", "George", "Joshua", "Kevin", "Brian", "Edward",
             "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan", "Gary", "Jacob", "Nicholas", "Eric", "Stephen",
             "Jonathan", "Larry", "Justin", "Scott", "Frank", "Brandon", "Raymond", "Gregory", "Benjamin",
             "Samuel", "Patrick", "Alexander", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Henry", "Douglas",
             "Jose", "Peter", "Adam", "Zachary", "Nathan", "Walter", "Harold", "Kyle", "Carl", "Arthur", "Gerald",
             "Roger", "Keith", "Jeremy", "Terry", "Lawrence", "Sean", "Christian", "Albert", "Joe", "Ethan", "Austin",
             "Jesse", "Willie", "Billy", "Bryan", "Bruce", "Jordan", "Ralph", "Roy", "Noah", "Dylan", "Eugene",
             "Wayne", "Alan", "Juan", "Louis", "Russell", "Gabriel", "Randy", "Philip", "Harry", "Vincent", "Bobby",
            "Johnny", "Logan" };

        static readonly string[] FemailFirstNames = { "Mary", "Patricia", "Jennifer", "Elizabeth", "Linda", "Barbara",
            "Susan", "Jessica", "Margaret", "Sarah", "Karen", "Nancy", "Betty", "Lisa", "Dorothy", "Sandra", "Ashley",
            "Kimberly", "Donna", "Carol", "Michelle", "Emily", "Amanda", "Helen", "Melissa", "Deborah", "Stephanie",
            "Laura", "Rebecca", "Sharon", "Cynthia", "Kathleen", "Amy", "Shirley", "Anna", "Angela", "Ruth", "Brenda",
            "Pamela", "Nicole", "Katherine", "Virginia", "Catherine", "Christine", "Samantha", "Debra", "Janet", "Rachel",
            "Carolyn", "Emma", "Maria", "Heather", "Diane", "Julie", "Joyce", "Evelyn", "Frances", "Joan", "Christina",
            "Kelly", "Victoria", "Lauren", "Martha", "Judith", "Cheryl", "Megan", "Andrea", "Ann", "Alice", "Jean", "Doris",
            "Jacqueline", "Kathryn", "Hannah", "Olivia", "Gloria", "Marie", "Teresa", "Sara", "Janice", "Julia", "Grace",
            "Judy", "Theresa", "Rose", "Beverly", "Denise", "Marilyn", "Amber", "Madison", "Danielle", "Brittany", "Diana",
            "Abigail", "Jane", "Natalie", "Lori", "Tiffany", "Alexis", "Kayla" };

        static readonly string[] LastNames = { "SMITH", "JOHNSON", "WILLIAMS", "BROWN", "JONES", "MILLER", "DAVIS", "GARCIA",
            "RODRIGUEZ", "WILSON", "MARTINEZ", "ANDERSON", "TAYLOR", "THOMAS", "HERNANDEZ", "MOORE", "MARTIN", "JACKSON",
            "THOMPSON", "WHITE", "LOPEZ", "LEE", "GONZALEZ", "HARRIS", "CLARK", "LEWIS", "ROBINSON", "WALKER", "PEREZ", "HALL",
            "YOUNG", "ALLEN", "SANCHEZ", "WRIGHT", "KING", "SCOTT", "GREEN", "BAKER", "ADAMS", "NELSON", "HILL", "RAMIREZ",
            "CAMPBELL", "MITCHELL", "ROBERTS", "CARTER", "PHILLIPS", "EVANS", "TURNER", "TORRES", "PARKER", "COLLINS", "EDWARDS",
            "STEWART", "FLORES", "MORRIS", "NGUYEN", "MURPHY", "RIVERA", "COOK", "ROGERS", "MORGAN", "PETERSON", "COOPER",
            "REED", "BAILEY", "BELL", "GOMEZ", "KELLY", "HOWARD", "WARD", "COX", "DIAZ", "RICHARDSON", "WOOD", "WATSON", "BROOKS",
            "BENNETT", "GRAY", "JAMES", "REYES", "CRUZ", "HUGHES", "PRICE", "MYERS", "LONG", "FOSTER", "SANDERS", "ROSS", "MORALES",
            "POWELL", "SULLIVAN", "RUSSELL", "ORTIZ", "JENKINS", "GUTIERREZ", "PERRY", "BUTLER", "BARNES", "FISHER", "HENDERSON",
            "COLEMAN", "SIMMONS", "PATTERSON", "JORDAN", "REYNOLDS", "HAMILTON", "GRAHAM", "KIM", "GONZALES", "ALEXANDER", "RAMOS",
            "WALLACE", "GRIFFIN", "WEST", "COLE", "HAYES", "CHAVEZ", "GIBSON", "BRYANT", "ELLIS", "STEVENS", "MURRAY", "FORD",
            "MARSHALL", "OWENS", "MCDONALD", "HARRISON", "RUIZ", "KENNEDY", "WELLS", "ALVAREZ", "WOODS", "MENDOZA", "CASTILLO",
            "OLSON", "WEBB", "WASHINGTON", "TUCKER", "FREEMAN", "BURNS", "HENRY", "VASQUEZ", "SNYDER", "SIMPSON", "CRAWFORD",
            "JIMENEZ", "PORTER", "MASON", "SHAW", "GORDON", "WAGNER", "HUNTER", "ROMERO", "HICKS", "DIXON", "HUNT", "PALMER",
            "ROBERTSON", "BLACK", "HOLMES", "STONE", "MEYER", "BOYD", "MILLS", "WARREN", "FOX", "ROSE", "RICE", "MORENO",
            "SCHMIDT", "PATEL", "FERGUSON", "NICHOLS", "HERRERA", "MEDINA", "RYAN", "FERNANDEZ", "WEAVER", "DANIELS", "STEPHENS",
            "GARDNER", "PAYNE", "KELLEY", "DUNN", "PIERCE", "ARNOLD", "TRAN", "SPENCER", "PETERS", "HAWKINS", "GRANT", "HANSEN",
            "CASTRO", "HOFFMAN", "HART", "ELLIOTT", "CUNNINGHAM", "KNIGHT" };
    }
}
