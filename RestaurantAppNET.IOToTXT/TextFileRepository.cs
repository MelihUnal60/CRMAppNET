using CRMAppNET.Domain.Entities;
using CRMAppNET.Domain.Entities.Base;
using CRMAppNET.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRMAppNET.Infrastructure.IOToTXT
{
    internal class TextFileRepository<T> : IRepository<T> where T : BaseEntity
    {
        private static string FileName
        {
            get
            {
                return typeof(T).FullName.Replace(".", "") + ".txt";
            }
        }

        private static List<T> list = new List<T>();

        private static void LoadListFromFile()
        {
            if(!File.Exists(FileName)) 
            {
                list = new List<T>();
                return;
            }

            var json = File.ReadAllText(FileName);
            list = JsonSerializer.Deserialize<List<T>>(json);
        }

        private static void WriteListToTXT()
        {
            var jsonText = JsonSerializer.Serialize(list);
            File.WriteAllText(FileName, jsonText);
        }

        static TextFileRepository()
        {
            LoadListFromFile();
        }

        public T Add(T entity)
        {
            LoadListFromFile();
            list.Add(entity);
            WriteListToTXT();
            return entity;
            
        }

        public T GetById(int id)
        {
            LoadListFromFile();
            var entity = list.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public ICollection<T> GetList(Func<T, bool> expression = null)
        {
            LoadListFromFile();

            return expression == null? list : list.Where(expression).ToList(); //en temiz yazım


            //if (expression == null)
            //{
            //    return list;      //daha sade yazım
            //}
            
            //    return list.Where(expression).ToList();
            
            //if(expression== null) 
            // {
            //     return list;    //standart yazım
            // }
            // else
            // {
            //     return list.Where(expression).ToList();
            // }
        }

        public bool Remove(int id)
        {
            LoadListFromFile();
            var deletedEntity = list.FirstOrDefault(x => x.Id == id);

            if(deletedEntity != null)
            {
                list.Remove(deletedEntity);
                WriteListToTXT();
                return true;
            }
            return false;

        }

        public T Update(int id, T newEntity)
        {
            if (id != newEntity.Id)
                throw new ArgumentException("Id değerleri eşleşmiyor!!");

            LoadListFromFile();
            var updatedEntity = list.FirstOrDefault(x => x.Id == id);
            if(updatedEntity == null) 
            {
                throw new Exception("Güncellenmek istenilen varlık bulunamadı");

            }

            list.Remove(updatedEntity);
            list.Add(newEntity);
            WriteListToTXT();
            return newEntity;
            

        }
    }
}
