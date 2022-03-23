﻿using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace GrammarEditor.ViewModels
{
    [QueryProperty(nameof(MSG_ID), nameof(MSG_ID))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string msg_id = string.Empty;
        private string msg_en = string.Empty;
        private string msg_ru = string.Empty;
        private string msg = string.Empty;
        private string msg_cat = string.Empty;
        private string ru_status = string.Empty;
        private string en_status = string.Empty;
        private string notes = string.Empty;
        private string bibliography = string.Empty;
        private string categories = string.Empty;

        public ItemDetailViewModel()
        {
            ExportToJSON = new Command(() => ExportToJSONAsync());
        }
        public ICommand ExportToJSON { get; }
        public async void ExportToJSONAsync()
        {
            var item = await DataStore.GetItemAsync(MSG_ID);
            item.MSG_EN = MSG_EN;
            item.MSG_RU = MSG_RU;
            item.MSG = MSG;
            item.NOTES = NOTES;
            item.CATEGORIES = CATEGORIES;
            item.MSG_CAT = MSG_CAT;
            item.RU_STATUS = RU_STATUS;
            item.EN_STATUS = EN_STATUS;
            item.BIBLIOGRAPHY = BIBLIOGRAPHY;

            //string json = JsonConvert.SerializeObject(item);
            string json = JsonConvert.SerializeObject(DataStore);
        }


        public string MSG { get=> msg; set => SetProperty(ref msg, value); }
        public string MSG_CAT { get => msg_cat; set => SetProperty(ref msg_cat, value); }
        public string RU_STATUS { get => ru_status; set => SetProperty(ref ru_status, value); }
        public string EN_STATUS { get => en_status; set => SetProperty(ref en_status, value); }
        public string NOTES { get => notes; set => SetProperty(ref notes, value); }
        public string BIBLIOGRAPHY { get => bibliography; set => SetProperty(ref bibliography, value); }
        public string CATEGORIES { get => categories; set => SetProperty(ref categories, value); }

        public string MSG_EN
        {
            get
            {
                return FormatGrammar(msg_en);
            }
            set => SetProperty(ref msg_en, value.Replace("<b>", "").Replace("</b>", ""));
        }

        public string MSG_RU
        {
            get => FormatGrammar(msg_ru);
            set => SetProperty(ref msg_ru, value.Replace("<b>", "").Replace("</b>", ""));
        }

        public string MSG_ID
        {
            get
            {
                return msg_id;
            }
            set
            {
                SetProperty(ref msg_id, value);
                LoadItemId(value);
            }
        }

        private string FormatGrammar(string GrammarText)
        {
            //TODO: created a custom renderer for the Editor control to be able to apply text formatting
            string sFormatted = GrammarText;//.Replace("[", "<b>[").Replace("]", "]</b>");

            return sFormatted;
        }


        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                MSG_EN = item.MSG_EN;
                MSG_RU = item.MSG_RU;
                MSG = item.MSG;
                NOTES = item.NOTES;
                CATEGORIES = item.CATEGORIES;
                MSG_CAT = item.MSG_CAT;
                RU_STATUS = item.RU_STATUS;
                EN_STATUS = item.EN_STATUS;
                BIBLIOGRAPHY = item.BIBLIOGRAPHY;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
