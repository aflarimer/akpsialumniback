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
            string sql = "select p.id, p.first_name, p.last_name, p.major, p.minor, p.pledge_class, p.graduating_semester, p.grad_school, p.grad_school_name, p.employed, p.position, p.company, p.city, p.email, p.phone, p.linkedIn, ifnull(c.lat,'') as lat, ifnull(c.lng, '') as lng from person p left join cities c on c.name = p.city order by p.last_name asc;";            
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
            var values = GetValues(person);
            string sql = "delete from person where id = @id";
            db.Open();
            db.Delete(sql, values);
            db.Close();
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
            string sql = "UPDATE person SET ";
            var values = GetValues(person);
            foreach(var p in values) {
                if (p.Key != "ID" && p.Value != null) {
                    switch(p.Key) {
                        case "@firstName":
                            sql += "first_name = @firstName,";
                            break;
                        case "@lastName":
                            sql += "last_name = @lastName,";
                            break;
                        case "@pledgeClass":
                            sql += "pledge_class = @pledgeClass,";
                            break;
                        case "@major":
                            sql += "major = @major,";
                            break;
                        case "@linkedIn":
                            sql += "linkedIn = @linkedIn,";
                            break;
                        case "@email":
                            sql += "email = @email,";
                            break;
                        case "@city":
                            sql += "city = @city,";
                            break;
                        case "@company":
                            sql += "company = @company,";
                            break;
                    }
                }
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql += " WHERE id = @id";
            db.Open();
            db.Update(sql, values);
            db.Close();
            // System.Console.WriteLine("made it to the update" + person.FirstName);
            // var values = GetValues(person);
            // string sql = "update person set first_name=@firstName, last_name=@lastName, major=@major,";
            // sql += "minor=@minor, pledge_class=@pledgeClass, graduating_semester=@graduatingSemester, grad_school=@gradSchool,";
            // sql+= "grad_school_name=@gradSchoolName, employed=@employed, position=@position, company=@company, city=@city, linkedIn=@linkedIn, email=@email, phone=@phone";
            // sql+= "where id = @id";
            // db.Open();
            // db.Update(sql, values);
            // db.Close();
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