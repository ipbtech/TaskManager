﻿namespace TaskManager.Api.Dto.Base
{
    public class BaseResponce<T>
    {
        public bool IsOkay { get; set; }
        public T? Data { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}