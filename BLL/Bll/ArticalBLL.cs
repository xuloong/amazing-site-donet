using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace BLL
{
    public class ArticleBLL
    {
        ArticleDAL articleDAL = new ArticleDAL();

        public List<ArticleDto> getPageList(int pageSize, int pageIndex, out int total, string keywords)
        {
            int totalOut;
            List<Article> articleList = articleDAL.getPageList(pageSize, pageIndex, out totalOut, keywords);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Article, ArticleDto>();
            });
            List<ArticleDto> articleDtoList = Mapper.Map<List<ArticleDto>>(articleList);
            total = totalOut;
            return articleDtoList;
        }

        public ArticleDto getById(int id)
        {
            Article article = articleDAL.getById(id);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Article, ArticleDto>();
            });
            ArticleDto articleDto = Mapper.Map<ArticleDto>(article);
            return articleDto;
        }

        public ArticleDto insert(ArticleDto articleDto, int createUserId)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ArticleDto, Article>();
            });
            Article article = Mapper.Map<Article>(articleDto);
            article.CreateUserId = createUserId;
            if (articleDAL.insert(article) > 0)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Article, ArticleDto>();
                });
                return Mapper.Map<ArticleDto>(article);
            }
            else
                return null;
        }

        public ArticleDto update(ArticleDto articleDto, int updateUserId)
        {
            Article article = articleDAL.getById(articleDto.Id);
            article.Title = articleDto.Title;
            article.Abstract = articleDto.Abstract;
            article.Content = articleDto.Content;
            article.UpdateUserId = updateUserId;
            if (articleDAL.update(article) > 0)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Article, ArticleDto>();
                });
                return Mapper.Map<ArticleDto>(article);
            }
            else
                return null;
        }

        public int delete(int id, int deleteUserId)
        {
            if (articleDAL.delete(id, deleteUserId) > 0)
                return 1;
            else
                return 0;
        }
    }
}
