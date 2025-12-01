using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.Factory
{
    public class BookFactory
    {
        public IPaper Build(string json)
        {
            //dynamic is a generic data type like var, however it allows me to inspect the properties
            //of any anonymous object
            dynamic myBuiltObject = JsonConvert.DeserializeObject<dynamic>(json);
            if (myBuiltObject != null)
            {
                if (myBuiltObject.Volume != null)
                {
                    //this is a journal
                    Journal j = JsonConvert.DeserializeObject<Journal>(json);
                    return j;
                }
                else
                {
                    //this is a book
                    Book j = JsonConvert.DeserializeObject<Book>(json);
                    return j;
                }
            }
            return null;
        }

        //this (Entry) method will know
        // - what to build
        // - what to do to save
        // - where to save it
        public void BuildAndSave(string json,
            IBooksRepository bookRepository,
            JournalsRepository journalsRepository
            )
        {
            IPaper paper = Build(json);

            if (paper.GetType() == typeof(Book))
            {
                //call the BooksRepo
                bookRepository.Add((Book)paper);
            }
            else
            {
                //call the JournalsRepo
                journalsRepository.Add((Journal)paper);
            }
        }
    }
}
