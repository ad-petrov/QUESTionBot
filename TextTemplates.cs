﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUESTionBot
{
    static class TextTemplates
    {
        public static string message1 = "Привет! Меня зовут Q, я - Telegram-бот. " +
            "\n\nСегодня я буду вашим помощником. Сейчас начнется ваш маршрут. " +
            "Впереди еще много интересных и загадочных мест Петроградской стороны. " +
            "Несмотря на то, что историческим центром Санкт-Петербурга является его левобережная часть, именно на Петроградской стороне были заложены первые постройки города: " +
            "Петропавловская крепость на Заячьем острове, Дом Петра I, Троицкая площадь, " +
            "где располагался первый торговый порт, здесь же в 1721 году Петр I принял титул императора. " +
            "\nМы уверены, что вы уже успели посмотреть все основные достопримечательности Петроградской стороны, " +
            "но сегодня вы узнаете то, что не расскажут учебники истории. ";

        public static string message2 = "Правила QUESTion:" +
            "\n1. Баллы за правильные ответы на вопросы суммируются и учитываются при подсчете результатов команды.За правильное выполнение задания с вариантами ответов дается 1 балл, за задания повышенной сложности - от 2 до 5." +
            "\n2. Время прохождения полного маршрута учитывается при подсчете результатов команды;" +
            "\n3. На каждой станции маршрута будет определен свой таймер - время, за которое ваша команда должна успеть выполнить задание;" +
            "\n4. Для того, чтобы приступить к основному заданию на станции, необходимо выполнить предварительное задание для того, чтобы организаторы удостоверились, что команда достигла пункта назначения;" +
            "\n5. При выполнении некоторых заданий можно воспользоваться подсказкой, для этого появится специальная кнопка.Заметьте, если вы пользуетесь подсказкой, то теряете 1 балл." +
            "\n6. При возникновении трудностей, вопросов и спорных ситуаций пишите @katchern" +
            "\n7. Капитан несет ответственность за соблюдение правил ПДД всеми членами команды";

        public static string message3 = "Обращайте внимание на сообщения, которые я вам буду присылать, они могут помочь при решении некоторых задач"
            + "\nВыкладывайте фотографии команды, отмечайте @itmo.students, ставьте хэштег #QUESTion и получайте +2 балла. Не забудьте указать название своей команды!";


    }
}
