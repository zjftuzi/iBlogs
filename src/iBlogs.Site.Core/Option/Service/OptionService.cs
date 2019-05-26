﻿using System;
using System.Collections.Generic;
using System.Linq;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;

namespace iBlogs.Site.Core.Option.Service
{
    public class OptionService : IOptionService
    {
        private readonly IRepository<Options> _repository;
        private IDictionary<string, string> _options;

        public OptionService(IRepository<Options> repository)
        {
            _repository = repository;
            _options = new Dictionary<string, string>();
            TryReLoad();
        }

        public void TryReLoad()
        {
            try
            {
                _options.Clear();
                _options = _repository.GetAll().ToDictionary(o => o.Name, o => o.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }

        public void Set(string key, string value)
        {
            if (_options.ContainsKey(key))
                if (_options[key] == value)
                    return;
                else
                {
                    var entity = _repository.GetAll().FirstOrDefault(o => o.Name == key);
                    entity.Value = value;
                    _repository.Update(entity);
                    _options[key] = value;
                }
            else
            {
                _repository.Insert(new Options {Name = key, Value = value});
                _options.Add(key, value);
            }
        }

        public string Get(string key, string defaultValue = null)
        {
            key = key.Trim().ToLower();
            if (_options.TryGetValue(key, out string value))
                return value;
            return defaultValue;
        }

        /**
        * 保存配置
        *
        * @param key   配置key
        * @param value 配置值
        */
        public void saveOption(string key, string value)
        {
            if (stringKit.isNotBlank(key) && stringKit.isNotBlank(value))
            {
                Set(key, value);
            }
        }

        /**
         * 获取系统配置
         */
        public IDictionary<string, string> getOptions()
        {
            return _options;
        }

        public string getOption(string key)
        {
            return Get(key);
        }

        /**
         * 根据key删除配置项
         *
         * @param key 配置key
         */
        public void deleteOption(string key)
        {
            if (_options.ContainsKey(key))
            {
                _options.Remove(key);
                _repository.Delete(_repository.GetAll().FirstOrDefault(o=>o.Name==key));
            }
        }


    }
}
