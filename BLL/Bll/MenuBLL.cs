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
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
            });
            List<MenuDto> menuDtoList = Mapper.Map<List<MenuDto>>(menuList);
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

        public List<MenuDto> getList()
        {
            List<Menu> menuList = menuDAL.getList(null);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
            });
            List<MenuDto> menuDtoList = Mapper.Map<List<MenuDto>>(menuList);
            return menuDtoList;
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

        public MenuDto getById(int id)
        {
            Menu menu = menuDAL.getById(id);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
            });
            MenuDto menuDto = Mapper.Map<MenuDto>(menu);
            return menuDto;
        }

        public MenuDto insert(MenuDto menuDto, int createUserId)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MenuDto, Menu>();
            });
            Menu menu = Mapper.Map<Menu>(menuDto);
            menu.CreateUserId = createUserId;
            if (menuDAL.insert(menu) > 0)
            {
                DictionaryBLL dictionaryBLL = new DictionaryBLL();
                dictionaryBLL.upVersion("menu_version");
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Menu, MenuDto>();
                });
                return Mapper.Map<MenuDto>(menu);
            }
            else
                return null;
        }

        public MenuDto update(MenuDto menuDto, int updateUserId)
        {
            Menu menu = menuDAL.getById(menuDto.Id);
            menu.Name = menuDto.Name;
            menu.ParentId = menuDto.ParentId;
            menu.ArticleId = menuDto.ArticleId;
            menu.OrderByNum = menuDto.OrderByNum;
            menu.UpdateUserId = updateUserId;
            if (menuDAL.update(menu) > 0)
            {
                DictionaryBLL dictionaryBLL = new DictionaryBLL();
                dictionaryBLL.upVersion("menu_version");
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Menu, MenuDto>();
                });
                return Mapper.Map<MenuDto>(menu);
            }
            else
                return null;
        }

        public int delete(int id, int deleteUserId)
        {
            if (menuDAL.delete(id, deleteUserId) > 0)
                return 1;
            else
                return 0;
        }
    }
}
