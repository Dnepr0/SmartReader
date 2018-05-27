﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartReader.Message;
using SmartReader.Message.Implementations;
using SmartReader.Networking;
using SmartReader.Database;
using System.Security.Cryptography;

namespace SmartReader.Server
{
    // TODO REMOVE PUBLIC
    public static partial class Program
    {
        #region Helpers
        private static string Hash(string word)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(word.ToStream());
                

                for (int i = 0; i < bytes.Length; i++)

                {

                    sb.Append(bytes[i].ToString("X2"));

                }

                
            }
            return sb.ToString();
        }

        private static string MakeToken(string login)
        {
            return Hash("001" + login + "rrrd");
        }

        private static void SendStatusError(IConnection connection, string message )
        {
            connection.Send(MessageFactory.MakeStatusMessage(Status.Error, message));
        }
        private static void SendStatusOk(IConnection connection, string message)
        {
            connection.Send(MessageFactory.MakeStatusMessage(Status.Ok, message));
        }
        #endregion

        public static void HandleAuthentication(IMessage message, IConnection connection)
        {
            AuthenticationMessage authMessage = message as AuthenticationMessage;
            string passHash = "";
            try
            {
                passHash = Conn.GetPersonPassHash(authMessage.Login);
            }
            catch(Exception e)
            {
                connection.Send(MessageFactory.MakeAuthenticationResposeMessage("", Status.Error, e.Message));
                return;
            }
            if (passHash == Hash(authMessage.Password))
            {
                SendToken(authMessage.Login, connection);
            } else
            {
                connection.Send(MessageFactory.MakeAuthenticationResposeMessage("", Status.Error, "Пароли не совпадают."));
            }
        }

        public static void HandleRegistration(IMessage message, IConnection connection)
        {
            RegistrationMessage regMessage = message as RegistrationMessage;
            if (regMessage.Password.IsEmpty())
            {
                connection.Send(MessageFactory.MakeAuthenticationResposeMessage("", Status.Error, "Пароль не может быть пустым."));
                return;
            }
            try
            {
                Conn.InsertUser(regMessage.Login, Hash(regMessage.Password));
                SendToken(regMessage.Login, connection);
            }
            catch(Exception e)
            {
                connection.Send(MessageFactory.MakeAuthenticationResposeMessage("", Status.Error, e.Message));
            }

        }

        private static void SendToken(string login, IConnection connection)
        {
            string token = Conn.GetTokenFor(login);
            if (token == null)
            {
                token = MakeToken(login);
                try
                {
                    Conn.InsertToken(login, token);
                }
                catch
                {
                    connection.Send(MessageFactory.MakeAuthenticationResposeMessage("", Status.Error, "Неизвестная ошибка!"));
                    return;
                }
            }
            else
            {
                connection.Send(MessageFactory.MakeAuthenticationResposeMessage(token));
            }
        }

        public static void HandleUploadBook(IMessage message, IConnection connection)
        {
            UploadBookMessage bookMessage = message as UploadBookMessage;
            string login = Conn.GetLoginFor(bookMessage.Token);
            if (login == null)
            {
                SendStatusError(connection, "Вы не авторизовались.");
                return;
            }

            // Upload the book
            try
            {
                // TODO реализовать
                Conn.InsertBookFor(login, bookMessage.Title, bookMessage.Content);
                SendStatusOk(connection, "Книга загружена.");
            }
            catch (Exception e)
            {
                SendStatusError(connection, e.Message);
            }
        }
    }
}
