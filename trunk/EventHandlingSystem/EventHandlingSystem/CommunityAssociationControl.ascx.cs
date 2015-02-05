﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using EventHandlingSystem.Database;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.FriendlyUrls;

namespace EventHandlingSystem
{
    public partial class CommunityAssociationControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCommunityDropDownList(DropDownListCommunity);
            }
        }

        #region Populate Methods

        //Metoden ska kunna användas av olika dropdownlistor
        public void PopulateCommunityDropDownList(DropDownList ddl)
        {
            ddl.Items.Clear();

            List<ListItem> commList = new List<ListItem>();

            //Lägg in alla communities i dropdownlista
            foreach (var comm in CommunityDB.GetAllCommunities())
            {
                commList.Add(new ListItem
                {
                    Text = comm.Name,
                    Value = comm.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            emptyItem.Attributes.Add("disabled", "disabled");
            ddl.Items.Add(emptyItem);

            //Sorterar commList i alfabetisk ordning i dropdownlistan för Communities
            foreach (var item in commList.OrderBy(item => item.Text))
            {
                ddl.Items.Add(item);
            }
        }


        //Metoden ska kunna användas av olika dropdownlistor
        public void PopulateAssociationDropDownList(DropDownList ddl)
        {
            ddl.Items.Clear();

            //Lägg in alla föreningar i dropdownlista
            List<ListItem> assoList = AssociationDB.GetAllAssociations().Select(asso => new ListItem
            {
                Text = asso.Name,  
                Value = asso.Id.ToString()
            }).ToList();

            // ALTERNATIV med foreach
            foreach (var asso in AssociationDB.GetAllAssociations())
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            ddl.Items.Add(emptyItem); //index 0

            //Sorterar assoList i alfabetisk ordning i dropdownlistan för Associations
            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ddl.Items.Add(item);
            }
        }



        public void PopulateAssociationListBox()
        {
            ListBoxAsso.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i listboxen
            foreach (
                var asso in
                    AssociationDB.GetAllAssociationsInCommunity(
                        CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ListBoxAsso.Items.Add(item);
            }
        }

        public void PopulateAssociationListBox(int aid)
        {
            ListBoxAsso.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i listboxen
            foreach (
                var asso in
                    AssociationDB.GetAllAssociationsInCommunity(
                        CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ListBoxAsso.Items.Add(item);
            }

            //Markera vilket item som ska vara vald när listan renderas
            ListBoxAsso.SelectedValue = aid.ToString();
        }

        //Visa associations i en community
        public void PopulateAssociationInCommunityDropDownList(DropDownList ddlPa, DropDownList ddlComm)
        {
            ddlPa.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i ddl
            foreach (var asso in AssociationDB.GetAllAssociationsInCommunity(CommunityDB.GetCommunityById(int.Parse(ddlComm.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            ddlPa.Items.Add(emptyItem); //index 0

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ddlPa.Items.Add(item);
            }
        }

        //Visa associations i en community, förvald/markerad association
        public void PopulateAssociationInCommunityDropDownList(int aid, DropDownList ddl)
        {
            ddl.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i listboxen
            foreach (var asso in AssociationDB.GetAllAssociationsInCommunity(CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            ddl.Items.Add(emptyItem); //index 0

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ddl.Items.Add(item);
            }

            //Markera vilket item som ska vara vald när listan renderas
            ddl.SelectedValue = aid.ToString();
        }
        


        // Visa ALLA föreningstyper i ddl
        public void PopulateAllAssociationCategoriesDropDownList()
        {
            DropDownListCategories.Items.Clear();

            //Hämta alla kategorierna och lägg i dropdownlistan 
            List<ListItem> acList = new List<ListItem>();

            foreach (var category in  CategoryDB.GetAllCategories())
            {
                acList.Add(new ListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            DropDownListCategories.Items.Add(emptyItem); //index 0

            foreach (var item in acList.OrderBy(item => item.Text))
            {
                DropDownListCategories.Items.Add(item);
            }
        }


        public void PopulateSubAssociationsBulletedList()
        {
            BulletedListSubAssociations.Items.Clear();

            List<ListItem> subAssoList = new List<ListItem>();

            //Lägg in alla underföreningar i punktlistan
            foreach (var asso in
                AssociationDB.GetAllSubAssociationsByParentAssociationId(int.Parse(ListBoxAsso.SelectedItem.Value)))
            {
                subAssoList.Add(new ListItem
                {
                    Text = asso.Name,
                    //Value = asso.Id.ToString()
                });
            }

            foreach (var item in subAssoList.OrderBy(item => item.Text))
            {
                BulletedListSubAssociations.Items.Add(item);
            }
        }

        
        public void PopulateCategoriesInAssoListBox()
        {
            ListBoxCatInAsso.Items.Clear();

            //Hämta alla kategorier för den aktuella föreningen 
            List<ListItem> ciaList = new List<ListItem>();

            foreach (var category in
                AssociationDB.GetAllCategoriesForAssociationByAssociation
                (AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedItem.Value))))
            {
                ciaList.Add(new ListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }

            foreach (var item in ciaList.Distinct()
                                       .OrderBy(item => item.Text))
            {
                ListBoxCatInAsso.Items.Add(item);
            }
        }

        #endregion



        #region Show Methods

        public void ShowCommunityDetails()
        {
            // Visa Community Name i textboxen och i rubrik över föreningslista
            TextBoxCommName.Text = DropDownListCommunity.SelectedItem.Text;
            LabelAssoInComm.Text = "Associations in " + DropDownListCommunity.SelectedItem.Text;
            LabelAssoInComm.Style.Add(HtmlTextWriterStyle.Color, "black");

            // Visa Community logga med länk innehållande tooltip
            HyperLinkLogoCommunity.NavigateUrl =
                "/SitePage.aspx?id=" +
                (WebPageDB.GetWebPageByCommunityId(int.Parse(DropDownListCommunity.SelectedValue)) != null
                    ? WebPageDB.GetWebPageByCommunityId(int.Parse(DropDownListCommunity.SelectedValue)).Id.ToString()
                    : "") + "&type=C";
            HyperLinkLogoCommunity.Target = "_blank";
            HyperLinkLogoCommunity.ToolTip = "This link goes to the web page of " +
                                             DropDownListCommunity.SelectedItem.Text + ". (^-^)v";

            // Visa Created och Created By
            LabelCreated.Text = "<b>Created: </b>" +
                                CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                    .Created.ToShortDateString();

            LabelCreatedBy.Text = "<b>Created by: </b>" +
                                  CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                      .CreatedBy;
        }

        public void ShowCommunityDetails(communities comm)
        {
            // Visa Community Name i textboxen och i rubrik över föreningslista
            TextBoxCommName.Text = comm.Name;
            LabelAssoInComm.Text = "Associations in " + comm.Name;
            LabelAssoInComm.Style.Add(HtmlTextWriterStyle.Color, "black");

            // Visa Community logga med länk innehållande tooltip
            HyperLinkLogoCommunity.NavigateUrl =
                "/SitePage.aspx?id=" +
                (WebPageDB.GetWebPageByCommunityId(comm.Id) != null
                    ? WebPageDB.GetWebPageByCommunityId(comm.Id).Id.ToString()
                    : "") + "&type=C";
            HyperLinkLogoCommunity.Target = "_blank";
            HyperLinkLogoCommunity.ToolTip = "This link goes to the web page of " +
                                             comm.Name + ". (^-^)v";

            // Visa Created och Created By
            LabelCreated.Text = "<b>Created: </b>" +
                                CommunityDB.GetCommunityById(comm.Id)
                                    .Created.ToShortDateString();

            LabelCreatedBy.Text = "<b>Created by: </b>" +
                                  CommunityDB.GetCommunityById(comm.Id)
                                      .CreatedBy;
        }


        public void ShowAssociationDetails()
        {
            associations asso = AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedItem.Value));

            //Visa Association Name i textboxen
            TextBoxAssoName.Text = ListBoxAsso.SelectedItem.Text;

            //Visa Association logga plus web page link
            HyperLinkLogoAssociation.NavigateUrl =
                "/SitePage.aspx?id=" +
                (WebPageDB.GetWebPageByAssociationId(int.Parse(ListBoxAsso.SelectedValue)) != null
                    ? WebPageDB.GetWebPageByAssociationId(int.Parse(ListBoxAsso.SelectedValue)).Id.ToString()
                    : "") + "&type=A";
            
            HyperLinkLogoAssociation.Target = "_blank";
            HyperLinkLogoAssociation.ToolTip = "This link goes to the web page of " + ListBoxAsso.SelectedItem.Text +
                                               "! o(^O^)o ";

            // Visa Community-dropdownlista
            PopulateCommunityDropDownList(DropDownListCommunityInAsso);
            DropDownListCommunityInAsso.SelectedIndex =
                DropDownListCommunityInAsso.Items.IndexOf(
                    DropDownListCommunityInAsso.Items.FindByValue(asso.Communities_Id.ToString()));

            // Visa ParentAssociation-dropdownlista
            PopulateAssociationInCommunityDropDownList(asso.Id, DropDownListParentAsso);

            if (asso.ParentAssociationId == null)
            {
                //Om föreningen inte har en förälder ska dropdownlistan visa blankt
                DropDownListParentAsso.SelectedIndex = 0;
            }
            else
            {
                DropDownListParentAsso.SelectedIndex =
                    DropDownListParentAsso.Items.IndexOf(
                        DropDownListParentAsso.Items.FindByValue(asso.ParentAssociationId.ToString()));
            }

            // Visa alla kategorier i dropdownlista
            PopulateAllAssociationCategoriesDropDownList();
            PopulateCategoriesInAssoListBox();
            DropDownListCategories.SelectedIndex = 0;

            //if (AssociationDB.GetAllCategoriesForAssociationByAssociation(asso).Count == 0)
            //{
            //    //Om föreningen inte har en föreningstyp ska dropdownlistan visa blankt
            //    DropDownListCategories.SelectedIndex = 0;
            //}
            //else
            //{
            //    DropDownListCategories.SelectedIndex =
            //    DropDownListCategories.Items.IndexOf(
            //        DropDownListCategories.Items.FindByValue(AssociationDB.GetAllCategoriesForAssociationByAssociation(asso)[0].Id.ToString()));
            //}

            //CheckBoxListAssoType.Items.Clear();
            //BulletedListAssoType.Items.Clear();

            //foreach (var item in AssociationDB.GetAllCategoriesForAssociationByAssociation(asso).OrderBy(item => item.Text))
            //{
            //    CheckBoxListAssoType.Items.Add(new ListItem
            //    {
            //        Text = item.Name,
            //        Value = item.Id.ToString()
            //    });

            //    BulletedListAssoType.Items.Add(new ListItem
            //    {
            //        Text = item.Name,
            //        Value = item.Id.ToString()
            //    });
            //}
            //foreach (var item in CheckBoxListAssoType.OrderBy(item => item.Text))
            //{
            //    ddl.Items.Add(item);
            //}

            // Visa Created, Created By och Publishing TermSet
            LabelCreatedAsso.Text = "<b>Created: </b>" +
                                    AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue))
                                        .Created.ToShortDateString();
            LabelCreatedByAsso.Text = "<b>Created by: </b>" +
                                      AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue)).CreatedBy;

            // Visa underföreningar för en förening i en lista
            if (AssociationDB.GetAllSubAssociationsByParentAssociationId(asso.Id).Count != 0)
            {
                PopulateSubAssociationsBulletedList();
            }
            else
            {
                BulletedListSubAssociations.Items.Clear();
                ListItem emptyItem = new ListItem(" --- ", " ");
                BulletedListSubAssociations.Items.Add(emptyItem); //index 0
            }
        }
    
    #endregion
        


        #region DropDownLists OnSelectedIndexChanged

        protected void DropDownListCommunity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownListCommunity.SelectedValue))
            {
                //Visa community-dropdownlist
                MultiViewSelectComm.ActiveViewIndex = 0;

                //Visa community info
                MultiViewCommDetails.ActiveViewIndex = 0;
                ShowCommunityDetails();

                //Visa lista med föreningar
                PopulateAssociationListBox();
                MultiViewAssoDetails.ActiveViewIndex = -1;
            }
            else
            {
                //Visa endast community-dropdownlist-view --> GÖM alla andra
                MultiViewSelectComm.ActiveViewIndex = 0;
                MultiViewCommDetails.ActiveViewIndex = -1;
                MultiViewAssoDetails.ActiveViewIndex = -1;
                MultiViewAssoCreate.ActiveViewIndex = -1;
            }

            //Rensa Labeltexter
            LabelErrorMessage.Text = string.Empty;
            LabelCommSave.Text = string.Empty;
        }


        //Associationslistbox för en viss Community - när ett item klickas visas föreningsdetaljerna
        protected void ListBoxAsso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            MultiViewAssoDetails.ActiveViewIndex = 0;
            MultiViewAssoCreate.ActiveViewIndex = -1;
            ShowAssociationDetails();
            LabelUpdateAsso.Text = "";
        }


        //Dropdownlista för Communities i Create Association View
        protected void DropDownListCommCreateAsso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownListCommunityCreateAsso.SelectedValue))
            {

            }
        }


        //Dropdownlista för Parent Associations 
        protected void DropDownListCreateParAsso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        
        //Dropdownlista för Association Types
        protected void DropDownListCreateAssoType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        #endregion



        #region Button Click

        protected void ButtonEditAsso_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ListBoxAsso.SelectedValue))
            {
                MultiViewAssoCreate.ActiveViewIndex = -1;
                MultiViewAssoDetails.ActiveViewIndex = 0;
                ShowAssociationDetails();
                LabelErrorMessage.Text = "";
            }
            else
            {
                LabelErrorMessage.Text = "You have to select an association!";
                LabelErrorMessage.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

        

        protected void ButtonCommSave_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownListCommunity.SelectedValue))
            {
                //Hitta community Id i dropdownlista - value
                int commId = int.Parse(DropDownListCommunity.SelectedItem.Value);

                //Uppdatera det nya namnet från textboxen
                communities commToUpdate = CommunityDB.GetCommunityById(commId);
                commToUpdate.Name = TextBoxCommName.Text;


                int affectedRows = CommunityDB.UpdateCommunity(commToUpdate);
                
                if (affectedRows != 0)
                {
                    LabelCommSave.Text = TextBoxCommName.Text + " has been updated.";
                    LabelCommSave.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                    PopulateCommunityDropDownList(DropDownListCommunity);
                }
                else
                {
                    LabelUpdateAsso.Text = "Error: " + TextBoxAssoName.Text +
                        " could not be updated!";
                    LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            else
            {
                LabelCommSave.Text = "Select a community before trying to save changes again.";
                LabelCommSave.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }


        // För att komma till "Create New Community" vy
        protected void ButtonCreateNewComm_OnClick(object sender, EventArgs e)
        {
            MultiViewCommCreate.ActiveViewIndex = 0;
        }

        //För att skapa en ny community
        protected void ButtonCreateComm_OnClick(object sender, EventArgs e)
        {
            communities comm = new communities
            {
                Name = TextBoxCommNameCreate.Text,
                LogoUrl = TextBoxCommLogoUrl.Text,
                CreatedBy = HttpContext.Current.User.Identity.Name,
                UpdatedBy = HttpContext.Current.User.Identity.Name
            };

            if (CommunityDB.AddCommunity(comm))
            {
                webpages wp = new webpages
                {
                    Title = TextBoxCommNameCreate.Text,
                    CommunityId = CommunityDB.GetCommunityByName(comm.Name).Id,
                    //Layout och style - fixa dropdownlistor senare!
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    UpdatedBy = HttpContext.Current.User.Identity.Name
                };

                if (WebPageDB.AddWebPage(wp))
                {
                    MultiViewCommDetails.ActiveViewIndex = 0;
                    MultiViewCommCreate.ActiveViewIndex = -1;
                    ShowCommunityDetails(CommunityDB.GetCommunityByName(comm.Name));
                }
                else
                {
                    LabelCreateComm.Text = "Webpage could not be created. Try again!";
                    LabelCreateComm.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            else
            {
                LabelCreateComm.Text = "Community could not be created. Try again!";
                LabelCreateComm.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

        //För att gå ur "Create New Community" vyn
        protected void ButtonCreateCommCancel_OnClick(object sender, EventArgs e)
        {
            MultiViewCommCreate.ActiveViewIndex = -1;
        }

        // För att komma till "Create New Association" vy
        protected void ButtonCreateNewAsso_OnClick(object sender, EventArgs e)
        {
            MultiViewAssoDetails.ActiveViewIndex = -1;
            MultiViewAssoCreate.ActiveViewIndex = 0;
            PopulateCommunityDropDownList(DropDownListCommunityCreateAsso);

            //Populera ParentAssociation dropdownlist
            DropDownListCreateParAsso.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            int commId;
            if (!string.IsNullOrWhiteSpace(DropDownListCommunityCreateAsso.SelectedItem.Value))
            {
                commId = int.Parse(DropDownListCommunityCreateAsso.SelectedItem.Value);
            }
            else
            {
                commId = DropDownListCommunityCreateAsso.SelectedIndex = 1;
            }
        }
        
        //Cancel Create Association View
        protected void ButtonCreateAssoCancel_OnClick(object sender, EventArgs e)
        {
            MultiViewAssoCreate.ActiveViewIndex = -1;
        }
        
        // För att SKAPA en ny förening
        protected void ButtonCreateAsso_OnClick(object sender, EventArgs e)
        {
            
        }
        
        // För att lägga till kategori(er) i listboxen
        protected void ButtonCatAdd_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownListCategories.SelectedValue))
            {
                if (!ListBoxCatInAsso.Items.Contains(DropDownListCategories.SelectedItem))
                {
                    ListBoxCatInAsso.Items.Add(DropDownListCategories.SelectedItem);
                }
                else
                {
                    LabelUpdateAsso.Text = "You cannot add the same category twice!";
                    LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            else
            {
                LabelUpdateAsso.Text = "You cannot add an empty category! Try again. ";
                LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }


        // För att ta bort kategori(er) i listboxen
        protected void ButtonCatRemove_OnClick(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = ListBoxCatInAsso.GetSelectedIndices().Select(index => (ListBoxCatInAsso.Items[index])).ToList();

            foreach (ListItem itemToRemove in itemsToRemove)
            {
                ListBoxCatInAsso.Items.Remove(itemToRemove);
            }
        }


        private int affectedRows;

        // För att spara ändringar i Association details - UPDATE-knappen
        protected void ButtonUpdateAsso_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ListBoxAsso.SelectedValue))
            {
                //Hitta Association-Id i listboxen - value
                int assoId = int.Parse(ListBoxAsso.SelectedItem.Value);


                // UPPDATERA det nya namnet från textboxen
                associations assoToUpdate = AssociationDB.GetAssociationById(assoId);
                assoToUpdate.Name = TextBoxAssoName.Text;

                PopulateAssociationListBox(assoId);


                // UPPDATERA community i vilken föreningen finns
                assoToUpdate.Communities_Id = int.Parse(DropDownListCommunityInAsso.SelectedItem.Value);

                
                // UPPDATERA ParentAssociation - omflyttningar i strukturen
                associations newParentAsso = new associations();
                
                if (assoToUpdate.ParentAssociationId == null) //assoToUpdate är en parentAsso, flyttar neråt eller under en annan asso
                {
                    if (!string.IsNullOrWhiteSpace(DropDownListParentAsso.SelectedItem.Value))
                    {
                        newParentAsso.Id = int.Parse(DropDownListParentAsso.SelectedItem.Value);

                        if (assoToUpdate.Id != newParentAsso.Id) //Får inte välja sig själv
                        {
                            //Om assoToUpdate flyttar till en annan ParentAsso behåller den sina barn
                            if (newParentAsso.ParentAssociationId == null)
                            {
                                //Får ny PAId                                
                                assoToUpdate.ParentAssociationId = newParentAsso.Id;
                            }
                            else //om den flyttar under sig själv eller till en childAsso
                            {
                                //Hitta alla barnen för att släppa dem. De blir alla parentAssos och får PA = null
                                foreach (var subAsso in AssociationDB.GetAllSubAssociationsByParentAssociationId(assoToUpdate.Id))
                                {
                                    subAsso.ParentAssociationId = null;
                                    affectedRows += AssociationDB.UpdateAssociation(subAsso);
                                }
                            }
                        }
                        else
                        {
                            LabelUpdateAsso.Text = TextBoxAssoName.Text +
                                " cannot be Parent Association to itself. Please try again. \n";
                            LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                        }
                    }
                    else
                    {
                        //Om man väljer blankt i ddl blir ParentAssociation null
                        assoToUpdate.ParentAssociationId = null;
                    }
                }
                else //assoToUpdate är en childAsso
                {
                    int? oldPAId = assoToUpdate.ParentAssociationId;

                    //assoToUpdate får ny PAId, får inte välja sig själv
                    if (assoToUpdate.Id != int.Parse(DropDownListParentAsso.SelectedItem.Value))
                    {
                        assoToUpdate.ParentAssociationId = newParentAsso.Id;
                    }
                    else
                    {
                        LabelUpdateAsso.Text = TextBoxAssoName.Text + " cannot be Parent Association to itself. Please try again. \r\n";
                        LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                    }

                    if (assoToUpdate.ParentAssociationId > oldPAId) //assoToUpdate flyttar neråt
                    {
                        //Hitta barnen och ge dem assoToUpdates gamla PAId
                        foreach (var subAsso in AssociationDB.GetAllSubAssociationsByParentAssociationId(assoToUpdate.Id))
                        {
                            subAsso.ParentAssociationId = oldPAId;
                            affectedRows += AssociationDB.UpdateAssociation(subAsso);
                        }
                    }
                }
                

                // UPPDATERA föreningskategori, lägg till de valda kategorierna från listboxen

                List<categories> catToAddList = new List<categories>();
                List<categories> catToRemoveList = new List<categories>();

                foreach (ListItem addedCategory in ListBoxCatInAsso.Items)
                {
                    categories category = CategoryDB.GetCategoryById(int.Parse(addedCategory.Value));

                    if (assoToUpdate.categories.Count != 0)
                    {
                        catToAddList.AddRange(from item in assoToUpdate.categories where item.Id != category.Id select category);
                    }
                    else
                    {
                        catToAddList.Add(category);
                    }
                }

                foreach (categories addItem in catToAddList)
                {
                    assoToUpdate.categories.Add(addItem);
                }

                // Föreningskategori - REMOVE
                foreach (var removedItem in assoToUpdate.categories)
                {
                    bool exist = false;

                    foreach (ListItem item in ListBoxCatInAsso.Items)
                    {
                        if (item.Value == removedItem.Id.ToString())
                        {
                            exist = true;
                            break;
                        }
                        exist = false;
                    }

                    if (!exist)
                    {
                        catToRemoveList.Add(removedItem);
                    }
                }

                foreach (categories removeItem in catToRemoveList)
                {
                    assoToUpdate.categories.Remove(removeItem);
                }
                
              
                //Anropa Update-metoden
                affectedRows = AssociationDB.UpdateAssociation(assoToUpdate);
                PopulateAssociationListBox();


                if (affectedRows != 0)
                {
                    LabelUpdateAsso.Text = TextBoxAssoName.Text + " has been updated!";
                    LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                }
                else
                {
                    LabelUpdateAsso.Text += "Error: Changes might not have been made in " + TextBoxAssoName.Text +
                                            "... Make sure to set the update information.";
                    LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            else
            {
                LabelUpdateAsso.Text = "Select an association in the listbox before trying to save changes again.";
                LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelAssoInComm.Text = "Select An Association in this Listbox";
                LabelAssoInComm.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

        private int _assoToDeleteId;

        // För att ta bort en association
        protected void ButtonDeleteAsso_OnClick(object sender, EventArgs e)
        {
            _assoToDeleteId = int.Parse(ListBoxAsso.SelectedItem.Value);

            //Om föreningen inte har några underföreningar
            if (AssociationDB.GetAllSubAssociationsByParentAssociationId(_assoToDeleteId).Count == 0)
            {
                //Ta även bort webpage för föreningen
                webpages webpageToDelete = WebPageDB.GetWebPageByAssociationId(_assoToDeleteId);

                //Anropa Delete-metoderna
                if (webpageToDelete != null)
                {
                    int affectedRows =
                        AssociationDB.DeleteAssociationById(_assoToDeleteId) + WebPageDB.DeleteWebPageByAssoId(_assoToDeleteId);

                    PopulateAssociationListBox();

                    if (affectedRows != 0)
                    {
                        LabelUpdateAsso.Text = TextBoxAssoName.Text + " was successfully deleted. ";
                        LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                    }
                    else
                    {
                        LabelUpdateAsso.Text = TextBoxAssoName.Text + "could not be deleted. Please try again.";
                        LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                    }
                }
            }
            //Visa Delete-view och lista på underföreningar som kommer att tas bort.
            else
            {
                MultiViewAssoDelete.ActiveViewIndex = 0;

                BulletedListSubAssoToDelete.Items.Clear();

                FindSubAssociationsAndAddtoDeleteList(AssociationDB.GetAssociationById(_assoToDeleteId));
            }
        }
        

        //Metod för att hitta alla under- och underföreningar
        protected void FindSubAssociationsAndAddtoDeleteList(associations asso)
        {
            List<ListItem> subAssoDeleteList = new List<ListItem>();

            //Lägg in alla underföreningar som ska tas bort i en itemlista
            foreach (var subasso in AssociationDB.GetAllSubAssociationsByParentAssociationId(asso.Id))
            {
                subAssoDeleteList.Add(new ListItem
                {
                    Text = subasso.Name,
                    Value = subasso.Id.ToString()
                });
            }

            //Lägg underföreningarna i ordning i punktlistan
            foreach (ListItem item in subAssoDeleteList.OrderBy(item => item.Text))
            {
                BulletedListSubAssoToDelete.Items.Add((item));

                //Hitta alla underföreningar och lägg till i listan - rekursiv
                FindSubAssociationsAndAddtoDeleteList(AssociationDB.GetAssociationById(int.Parse(item.Value)));
            }
        }
        
        protected void ButtonDeleteAsso2_OnClick(object sender, EventArgs e)
        {
            //Ta bort den valda föreningen i listboxen
            _assoToDeleteId = int.Parse(ListBoxAsso.SelectedItem.Value);

            int affectedRows = AssociationDB.DeleteAssociationById(_assoToDeleteId) + WebPageDB.DeleteWebPageByAssoId(_assoToDeleteId);
            
            // Ta bort underföreningarna
            foreach (ListItem itemToDelete in BulletedListSubAssoToDelete.Items)
            {
                affectedRows =+ 
                AssociationDB.DeleteAssociationById(int.Parse(itemToDelete.Value)) + WebPageDB.DeleteWebPageByAssoId(int.Parse(itemToDelete.Value));
            }

            if (affectedRows != 0)
            {
                LabelUpdateAsso.Text = TextBoxAssoName.Text + " (and all sub associations) was successfully deleted. ";
                LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
            }
            else
            {
                LabelUpdateAsso.Text = TextBoxAssoName.Text + "could not be deleted. Please try again.";
                LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
            }

            PopulateAssociationListBox();
            MultiViewAssoDelete.ActiveViewIndex = -1;
        }

        protected void ButtonDeleteAssoCancel_OnClick(object sender, EventArgs e)
        {
            MultiViewAssoDelete.ActiveViewIndex = -1;
        }


        #endregion

        
        
    }
}