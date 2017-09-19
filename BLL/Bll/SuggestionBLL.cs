using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace BLL
{
    public class SuggestionBLL
    {
        SuggestionDAL suggestionDAL = new SuggestionDAL();

        public List<SuggestionDto> getPageList(int pageSize, int pageIndex, out int total, string keywords)
        {
            int totalOut;
            List<Suggestion> suggestionList = suggestionDAL.getPageList(pageSize, pageIndex, out totalOut, keywords);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Suggestion, SuggestionDto>();
            });
            List<SuggestionDto> suggestionDtoList = Mapper.Map<List<SuggestionDto>>(suggestionList);
            total = totalOut;
            return suggestionDtoList;
        }

        public SuggestionDto getById(int id)
        {
            Suggestion suggestion = suggestionDAL.getById(id);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Suggestion, SuggestionDto>();
            });
            SuggestionDto suggestionDto = Mapper.Map<SuggestionDto>(suggestion);
            return suggestionDto;
        }

        public SuggestionDto insert(SuggestionDto suggestionDto, int createUserId)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SuggestionDto, Suggestion>();
            });
            Suggestion suggestion = Mapper.Map<Suggestion>(suggestionDto);
            suggestion.CreateUserId = createUserId;
            if (suggestionDAL.insert(suggestion) > 0)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Suggestion, SuggestionDto>();
                });
                return Mapper.Map<SuggestionDto>(suggestion);
            }
            else
                return null;
        }

        public int delete(int id, int deleteUserId)
        {
            if (suggestionDAL.delete(id, deleteUserId) > 0)
                return 1;
            else
                return 0;
        }
    }
}
