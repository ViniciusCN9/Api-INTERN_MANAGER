using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.api.Hateoas;
using DesafioAPI.api.Hateoas.Containers;
using DesafioAPI.domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.api.Helpers
{
    public class HateoasHelper
    {
        public HateoasHelper() { }

        public CategoryContainer CategoryGenerateLink(Category category, List<string> hrefList)
        {
            try
            {
                var categoryContainer = new CategoryContainer();

                categoryContainer.Category = category;
                categoryContainer.Links.Add(new HateoasLink(hrefList.ElementAt(0), "GET_CATEGORY", "GET"));
                categoryContainer.Links.Add(new HateoasLink(hrefList.ElementAt(1), "UPDATE_PARTIAL_CATEGORY", "PATCH"));
                categoryContainer.Links.Add(new HateoasLink(hrefList.ElementAt(2), "UPDATE_CATEGORY", "PUT"));
                categoryContainer.Links.Add(new HateoasLink(hrefList.ElementAt(3), "DELETE_CATEGORY", "DELETE"));

                return categoryContainer;
            }
            catch
            {
                throw new Exception("Falha ao criar links");
            }
        }

        public AccountContainer AccountGenerateLink(Account account, List<string> hrefList)
        {
            try
            {
                var accountContainer = new AccountContainer();

                accountContainer.Account = account;
                accountContainer.Links.Add(new HateoasLink(hrefList.ElementAt(0), "GET_ACCOUNT", "GET"));
                accountContainer.Links.Add(new HateoasLink(hrefList.ElementAt(1), "UPDATE_PARTIAL_ACCOUNT", "PATCH"));
                accountContainer.Links.Add(new HateoasLink(hrefList.ElementAt(2), "UPDATE_ACCOUNT", "PUT"));
                accountContainer.Links.Add(new HateoasLink(hrefList.ElementAt(3), "DELETE_ACCOUNT", "DELETE"));

                return accountContainer;
            }
            catch
            {
                throw new Exception("Falha ao criar links");
            }
        }

        public StarterContainer StarterGenerateLink(Starter starter, List<string> hrefList)
        {
            try
            {
                var starterContainer = new StarterContainer();

                starterContainer.Starter = starter;
                starterContainer.Links.Add(new HateoasLink(hrefList.ElementAt(0), "GET_STARTER", "GET"));
                starterContainer.Links.Add(new HateoasLink(hrefList.ElementAt(1), "GET_STARTER_BY_NAME", "GET"));
                starterContainer.Links.Add(new HateoasLink(hrefList.ElementAt(2), "UPLOAD_PHOTO_STARTER", "POST"));
                starterContainer.Links.Add(new HateoasLink(hrefList.ElementAt(3), "GET_PHOTO_STARTER", "POST"));
                starterContainer.Links.Add(new HateoasLink(hrefList.ElementAt(4), "UPDATE_PARTIAL_STARTER", "PATCH"));
                starterContainer.Links.Add(new HateoasLink(hrefList.ElementAt(5), "UPDATE_STARTER", "PUT"));
                starterContainer.Links.Add(new HateoasLink(hrefList.ElementAt(6), "DELETE_STARTER", "DELETE"));

                return starterContainer;
            }
            catch
            {
                throw new Exception("Falha ao criar links");
            }
        }

        public StarterContainer UserGenerateLink(Starter starter, List<string> hrefList)
        {
            try
            {
                var starterContainer = new StarterContainer();

                starterContainer.Starter = starter;
                starterContainer.Links.Add(new HateoasLink(hrefList.ElementAt(0), "GET_STARTER_BY_NAME", "GET"));
                starterContainer.Links.Add(new HateoasLink(hrefList.ElementAt(1), "GET_STARTER_PHOTO_BY_NAME", "GET"));

                return starterContainer;
            }
            catch
            {
                throw new Exception("Falha ao criar links");
            }
        }
    }
}