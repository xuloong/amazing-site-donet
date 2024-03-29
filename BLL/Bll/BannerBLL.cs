﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace BLL
{
    public class BannerBLL
    {
        BannerDAL bannerDAL = new BannerDAL();
        public List<BannerDto> getList(string type, string status)
        {
            List<Banner> bannerList = bannerDAL.getList(type, status);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Banner, BannerDto>();
            });
            List<BannerDto> bannerDtoList = Mapper.Map<List<BannerDto>>(bannerList);
            return bannerDtoList;
        }

        public BannerDto getById(int id)
        {
            Banner banner = bannerDAL.getById(id);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Banner, BannerDto>();
            });
            BannerDto bannerDto = Mapper.Map<BannerDto>(banner);
            return bannerDto;
        }

        public BannerDto insert(BannerDto bannerDto, int createUserId)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BannerDto, Banner>();
            });
            Banner banner = Mapper.Map<Banner>(bannerDto);
            banner.CreateUserId = createUserId;
            if (bannerDAL.insert(banner) > 0)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Banner, BannerDto>();
                });
                return Mapper.Map<BannerDto>(banner);
            }
            else
                return null;
        }

        public BannerDto update(BannerDto bannerDto, int updateUserId)
        {
            Banner banner = bannerDAL.getById(bannerDto.Id);
            banner.Type = bannerDto.Type;
            banner.ImageSrc = bannerDto.ImageSrc;
            banner.Link = bannerDto.Link;
            banner.Status = bannerDto.Status;
            banner.OrderByNum = bannerDto.OrderByNum;
            banner.UpdateUserId = updateUserId;
            if (bannerDAL.update(banner) > 0)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Banner, BannerDto>();
                });
                return Mapper.Map<BannerDto>(banner);
            }
            else
                return null;
        }

        public int delete(int id, int deleteUserId)
        {
            if (bannerDAL.delete(id, deleteUserId) > 0)
                return 1;
            else
                return 0;
        }

    }
}
