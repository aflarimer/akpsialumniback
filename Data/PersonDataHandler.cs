using System;
using System.Collections.Generic;
using api.Interfaces;
using api.Model;
using System.Dynamic;
namespace api.Data
{
    public class PersonDataHandler : IPersonDataHandler
    {
        private Database db;
        public PersonDataHandler() {
            db = new Database();
        }
         public List<Person> Select() {
            List<Person> myPeople = new List<Person>();
            string sql = "SELECT p.id, first_name, last_name, major, minor, pledge_class, graduating_semester, grad_school, grad_school_name, employed, position, company, city, email, phone, linkedIn, lat, lng FROM cities c join person p where c.name = p.city";
            db.Open();
            List<ExpandoObject> results = db.Select(sql);
            foreach(dynamic item in results) {
                Person temp = new Person(){ID = item.id, 
                    FirstName = item.first_name, 
                    LastName = item.last_name, 
                    Major = item.major, 
                    Minor = item.minor, 
                    PledgeClass = item.pledge_class, 
                    GraduatingSemester = item.graduating_semester, 
                    GradSchool = item.grad_school, 
                    GradSchoolName = item.grad_school_name, 
                    Employed = item.employed, 
                    Position = item.position,
                    Company = item.company, 
                    LinkedIn = item.linkedIn,
                    City = item.city, 
                    Email = item.email,
                    Phone = item.phone,
                    Latitude = item.lat, //
                    Longitude = item.lng}; //
                myPeople.Add(temp);
                
            }
            db.Close();
            return myPeople;
         }
         public void Delete(Person person){

         }
         public void Insert(Person person){
            //Console.WriteLine("made it to the insert"); for testing
            
            var values = GetValues(person);
            string sql = "insert into person (first_name, last_name, major, minor, pledge_class, graduating_semester, grad_school, grad_school_name, employed, position, company, city, linkedIn, email, phone) ";
            sql+= "values(@firstName, @lastName, @major, @minor, @pledgeClass, @graduatingSemester, @gradSchool, @gradSchoolName, @employed, @position, @company, @city, @linkedIn, @email, @phone)";
            db.Open();
            db.Insert(sql, values);
            db.Close();
         }
         public void Update(Person person){
            System.Console.WriteLine("made it to the update" + person.FirstName);
            var values = GetValues(person);
            string sql = "update person set first_name=@firstName, last_name=@lastName, major=@major,";
            sql += "minor=@minor, pledge_class=@pledgeClass, graduating_semester=@graduatingSemester, grad_school=@gradSchool,";
            sql+= "grad_school_name=@gradSchoolName, employed=@employed, position=@position, company=@company, city=@city, linkedIn=@linkedIn, email=@email, phone=@phone";
            sql+= "where id = @id";
            db.Open();
            db.Update(sql, values);
            db.Close();
         }

         public Dictionary<string,object> GetValues(Person person) {
             var values = new Dictionary<string,object>(){
                 {"@id", person.ID},
                 {"@firstName", person.FirstName},
                 {"@lastName", person.LastName},
                 {"@major", person.Major},
                 {"@minor", person.Minor},
                 {"@pledgeClass", person.PledgeClass},
                 {"@graduatingSemester", person.GraduatingSemester},
                 {"@gradSchool", person.GradSchool},
                 {"@gradSchoolName", person.GradSchoolName},
                 {"@employed", person.Employed},
                 {"@position", person.Position},
                 {"@company", person.Company},
                 {"@city", person.City},
                 {"@linkedIn", person.LinkedIn},
                 {"@email", person.Email},
                 {"@phone", person.Phone}
             };
             return values;

         }
    }
}