using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
             context.Authors.AddRange
                (
                    new Author { FirstName = "Eric", LastName = "Ries", Birthday = new DateTime(1978,09,22) },
                    new Author { FirstName = "Charlotte Perkins",  LastName = "Gilman", Birthday = new DateTime(1860,06,03) },
                    new Author {  FirstName = "Frank",  LastName = "Herbert",  Birthday = new DateTime(1920,10,08) }
                );
        }
    }
}

  
  
                       
                                       
                        
                        
                        