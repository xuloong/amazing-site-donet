using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace BLL
{
    public class MenuBLL
    {
        MenuDAL menuDAL = new MenuDAL();
        public List<MenuDto> getList(int? parentId)
        {
            List<Menu> menuList = menuDAL.getList(parentId);
            List<MenuDto> menuDtoList = new List<MenuDto>();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
            });
            foreach (Menu menu in menuList)
            {
                MenuDto menuDto = Mapper.Map<MenuDto>(menu);
                menuDtoList.Add(menuDto);
            }
            List<MenuDto> newMenuDtoList = new List<MenuDto>();
            foreach (MenuDto menuDto in menuDtoList)
            {
                if (menuDto.ParentId == null || menuDto.ParentId == 0)
                    newMenuDtoList.Add(menuDto);
            }
            foreach(MenuDto menuDto in newMenuDtoList)
            {
                menuDto.subMenu = this.getSubMenuList(menuDtoList, menuDto.Id);
            }

            return newMenuDtoList;
        }

        private List<MenuDto> getSubMenuList(List<MenuDto> menuDtoList, int? id)
        {
            List<MenuDto> subMenuDtoList = new List<MenuDto>();
            foreach (MenuDto menuDto in menuDtoList)
            {
                if (menuDto.ParentId == id)
                    subMenuDtoList.Add(menuDto);
            }
            foreach (MenuDto menuDto in subMenuDtoList)
            {
                menuDto.subMenu = this.getSubMenuList(menuDtoList, menuDto.Id);
            }
            if (subMenuDtoList.Count() == 0)
                return null;
            else
                return subMenuDtoList;
        }
    }
}
