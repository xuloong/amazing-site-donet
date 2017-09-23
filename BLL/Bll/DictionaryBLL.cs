using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace BLL
{
    public class DictionaryBLL
    {
        DictionaryDAL dictionaryDAL = new DictionaryDAL();

        public DictionaryDto getByKey(string key)
        {
            Dictionary dictionary = dictionaryDAL.getByKey(key);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Dictionary, DictionaryDto>();
            });
            DictionaryDto dictionaryDto = Mapper.Map<DictionaryDto>(dictionary);
            return dictionaryDto;
        }

        public DictionaryDto update(DictionaryDto dictionaryDto, int updateUserId)
        {
            Dictionary dictionary = dictionaryDAL.getByKey(dictionaryDto.Key);
            dictionary.Value = dictionaryDto.Value;
            if (dictionaryDAL.update(dictionary) > 0)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Dictionary, DictionaryDto>();
                });
                return Mapper.Map<DictionaryDto>(dictionary);
            }
            else
                return null;
        }

        public int upVersion(string key)
        {
            Dictionary dictionary = dictionaryDAL.getByKey(key);
            if (dictionary == null)
                return -1;
            dictionary.Value = (int.Parse(dictionary.Value) + 1).ToString();
            return dictionaryDAL.update(dictionary);
        }
    }
}
