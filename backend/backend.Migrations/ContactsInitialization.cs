﻿
using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Migrations
{
    public static class ContactsInitialization
    {
        public static void Init(this DbContext context)
        {
            List<ContactEntity> contacts = GetData();

            foreach (ContactEntity contact in contacts)
            {
                context.Add(contact);
            };
            context.SaveChanges();
        }

        private static List<ContactEntity> GetData()
        {
            return new List<ContactEntity>() 
            {
                new ContactEntity()
                {
                    Id = new Guid(),
                    Name = "ЕДИНЫЙ ТЕЛЕФОН ЭКСТРЕННЫХ СЛУЖБ",
                    Number = "112",
                    Description = "Номер 112 — это единый номер телефона для всех стран Европейского союза, по которому надо звонить в экстренных случаях. Данный номер появился на свет по инициативе Швеции и благодаря решению Совета Европы от 29 июля 1991 года. В России  «112» — стал единым номером вызова экстренных оперативных служб для приёма сообщений о пожарах и чрезвычайных ситуациях в телефонных сетях местной телефонной связи."
                },
                new ContactEntity()
                {
                    Id = new Guid(),
                    Name = "ПОЖАРНАЯ ОХРАНА И СПАСАТЕЛИ",
                    Number = "101",
                    Description = "Помни! Для вызова пожарной охраны в телефонных сетях населенных пунктов устанавливается единый номер — 101.\nА знаешь ли ты, что Пожарная служба занимается не просто тушением огня? Они осуществляют спасение людей и их имущества от огня и оказывают первичную врачебную помощь пострадавшим! Пожарные самые настоящие герои, каждый день рискующие своей жизнью, спасая людей.\nТы всегда можешь обратиться за помощью самостоятельно, главное запомни номер 101 и научись сохранять спокойствие и холодный разум в экстренной ситуации."
                },
                new ContactEntity()
                {
                    Id = new Guid(),
                    Name = "ПОЛИЦИЯ",
                    Number = "102",
                    Description = "Знай! Для вызова полиции в телефонных сетях населенных пунктов устанавливается единый номер — 102.\nЕсли тебе грозит опасность или ты стал свидетелем происшествия – нападения, ограбления, избиения и т.д. – сразу звони нашим доблестным защитникам!"
                },
                new ContactEntity()
                {
                    Id = new Guid(),
                    Name = "СКОРАЯ ПОМОЩЬ",
                    Number = "103",
                    Description = "Помни! Для вызова скорой помощи в телефонных сетях населенных пунктов устанавливается единый номер — 103.\nЭта служба отвечает за оказание врачебной помощи пострадавшим. Если тебе или кому-то рядом стало плохо, и срочно нужен врач – то следует звонить на номер 03."
                },
                new ContactEntity()
                {
                    Id = new Guid(),
                    Name = "СЛУЖБА ГАЗА",
                    Number = "104",
                    Description = "Знай! Для вызова службы газа в телефонных сетях населенных пунктов устанавливается единый номер — 104.\nВ Аварийную службу газа следует звонить, если ты почувствовал дома (в подъезде, на улице рядом с домом) сильный запах газа. Только помни, что звонить нужно не из загазованного помещения!\nЕсли ты сам вызываешь одну из служб, то запомни алгоритм того, что нужно говорить:\n- что случилось;\n- с кем это случилось;\n- где это случилось (адрес называй четко и разборчиво);\n- сообщи свое имя и фамилию.\nНе забывай, что вызов экстренных служб – это не игрушки, нельзя баловаться, обманывать операторов. Последствия ложного звонка могут быть очень серьезными: мало того, что твоим родителям придется выплачивать штраф (а тебя, скорее всего, накажут), но и в это время – пока специальные службы будут реагировать на твой обман, где-то могут пострадать люди, которым действительно требуется помощь."
                }
            };
        }
    }
}
