﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartReader.Message
{
    /// <summary>
    /// На каждый тип сообщения существует отдельный класс.
    /// Для того, чтобы использовать эти сообщения, необходимо:
    /// 1. Создать класс, соответсвующий типу сообщения, реализовав интерфейс IMessage.
    /// 2. Создать метод в MessageFactory для создания сообщения.
    /// 3. Создать обработчик на сервере, который будет обрабатывать сообщение.
    /// 4. (опционально) Создать обработчик в проекте Database, который будет сохранять данные.
    /// </summary>
    public enum MessageTypes
    {
        Authenticate,
        AuthenticateResponse,
        Registration,
        Status,
        UploadBook,
        GetBookList,
        BookList,
        GetBook,
        Book,
        DeleteBook,
        // Мы тут
        AddBookmark,
        DeleteBookmark,
    }
}
