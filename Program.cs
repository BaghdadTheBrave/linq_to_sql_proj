using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LINQ{
    class Program{
        static void Main(string[] args)
        {
            List<Manufacturer> manufacturers=new List<Manufacturer>{
                new Manufacturer{
                    id=1,
                    itemsId=new List<int>{1,3,5},
                    name ="Krokodyl Gena"},
                new Manufacturer{
                    id=2,
                    itemsId=new List<int>{2,4,8,10},
                    name ="Shapoklyak"},
                new Manufacturer{
                    id=3,
                    itemsId=new List<int>{6,7,9},
                    name ="Cheburashka"},
            };
            List<Item> items = new List<Item>{
                new Item{
                    id=1,
                    amount=20,
                    price=29,
                    name="Abobs",
                    deliveriesId=new List<int>{1,4,7,9},
                    Manufacturer=1
                },
                                new Item{
                    id=2,
                    amount=210,
                    price=229,
                    name="MnM",
                    deliveriesId=new List<int>{2,4,7,9},
                    Manufacturer=2
                },
                                new Item{
                    id=3,
                    amount=223,
                    price=1219,
                    name="KlKl",
                    deliveriesId=new List<int>{3,7,8},
                    Manufacturer=1
                },
                                new Item{
                    id=4,
                    amount=202,
                    price=291,
                    name="Ukbk",
                    deliveriesId=new List<int>{5,6,10},
                    Manufacturer=2
                },
                                new Item{
                    id=5,
                    amount=1,
                    price=212,
                    name="Jbs",
                    deliveriesId=new List<int>{2,4,5,6},
                    Manufacturer=1
                },
                                new Item{
                    id=6,
                    amount=545,
                    price=761,
                    name="wtr",
                    deliveriesId=new List<int>{1,2,3},
                    Manufacturer=3
                },
                                new Item{
                    id=7,
                    amount=220,
                    price=229,
                    name="Jabster",
                    deliveriesId=new List<int>{7,9},
                    Manufacturer=3
                },
                                new Item{
                    id=8,
                    amount=120,
                    price=219,
                    name="HE",
                    deliveriesId=new List<int>{1,9},
                    Manufacturer=2
                },
                                new Item{
                    id=9,
                    amount=999,
                    price=999,
                    name="Frogie frog",
                    deliveriesId=new List<int>{1,2,3,4,5,6,7,8,9,10},
                    Manufacturer=3
                },
                                new Item{
                    id=10,
                    amount=0,
                    price=1,
                    name="coin",
                    deliveriesId=new List<int>{1,2,7,9},
                    Manufacturer=2
                },
            };
            List<Deliveries> deliveries=new List<Deliveries>{};
            DateOnly[]dates= new DateOnly[9]{
                new DateOnly(2021, 2, 2),
                new DateOnly(2021, 2, 3),
                new DateOnly(2021, 2, 12),

                new DateOnly(2021, 3, 1),
                new DateOnly(2021, 3, 5),
                new DateOnly(2021, 3, 10),

                new DateOnly(2021, 3, 17),
                new DateOnly(2021, 3, 28),
                new DateOnly(2021, 4, 2)
            };
            for(int i=0;i<9;i++){
                deliveries.Add(new Deliveries(items,dates[i],i+1));
            }
            
            List<itemsDeliveries> itemsDeliveries = new List<itemsDeliveries>{};
            foreach (var item in items){
                foreach (var deliveryId in item.deliveriesId){
                    itemsDeliveries.Add(new itemsDeliveries{itemId=item.id, deliveryId=deliveryId});
                }
            }
            
            
            
            Query1(items);
            Query2(deliveries);
            Query3(items);
            Query4(items);
            Query5(items,manufacturers);
            Query6(items,manufacturers);
            Query7(deliveries,items,itemsDeliveries);
            Query8(items);
            Query9(items);
            Query10(items,manufacturers);
            Query11(items);
            Query12(manufacturers,items);
            Query13(items);
            Query14(items,manufacturers);
            Query15(items,deliveries);

        }
        static void Query1(List<Item> items){
            Console.WriteLine("showing each item:");
            var q1 = from item in items
                select item;
            foreach(var q in q1){
                Console.WriteLine(q.ToString());
            }
            Console.WriteLine();
        }
        static void Query2(List<Deliveries>deliveries){
            Console.WriteLine("showing each delivery after 10.10.2020, containing item with id 3:");
            var q1 = from delivery in deliveries 
                where delivery.date.CompareTo(new DateOnly(2020,10,10))>0 && delivery.items.Contains(3)
                select delivery.id;
            foreach(var q in q1){
                Console.WriteLine(q);
            }
            Console.WriteLine();
        }
        static void Query3(List<Item>items){
            Console.WriteLine("showing each item with total price less than 20000 in descending order:");
            var result = from item in items 
                where item.amount*item.price<20000
                orderby item.name descending
                select item;
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }
        static void Query4(List<Item>items){
            Console.WriteLine("showing each item grouping by manufacturer:");
            var result = from item in items
                group item by item.Manufacturer;
            foreach (var group in result){
                foreach(var item in group){
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine();
        }
        static void Query5(List<Item>items,List<Manufacturer>manufacturers){
            Console.WriteLine("showing each item with it's manufacturer name:");
            var result= from i in items
                join m in manufacturers on i.Manufacturer equals m.id
                
                select new{
                    name=i.name,
                    manufacturer=m.name
                };
            foreach(var r in result){
                Console.WriteLine("{0} is created by {1}",r.name, r.manufacturer);
            }   
            Console.WriteLine(); 
        }
        static void Query6(List<Item>items,List<Manufacturer>manufacturers){
            Console.WriteLine("showing each item with manufacturer's name first letter is S:");
            var result= from i in items
                join m in manufacturers on i.Manufacturer equals m.id
                where m.name[0]=='S'
                select new{
                    name=i.name,
                    manufacturer=m.name
                };
            foreach(var r in result){
                Console.WriteLine("{0} is created by {1}",r.name, r.manufacturer);
            }   
            Console.WriteLine(); 
        }
        static void Query7(List<Deliveries> deliveries,List<Item>items,List<itemsDeliveries>itemsDeliveries){
            Console.WriteLine("showing date of each delivery containing 'Abobs' item");
            var result = from itDl in itemsDeliveries
                join i in items on itDl.itemId equals i.id 
                join d in deliveries on itDl.deliveryId equals d.id
                where i.name=="Abobs" 
                select d.date;
            foreach(var r in result){
                Console.WriteLine("{0}/{1}/{2}",r.Day,r.Month,r.Year);
            }
        }
        static void Query8(List<Item>items){
            Console.WriteLine("showing items made by first manufacturer then by second");
            var result = items.Where(i => i.Manufacturer==1).Concat(items.Where(i => i.Manufacturer==2));
            foreach(var r in result){
                Console.WriteLine(r.ToString());
            }
        }
        static void Query9(List<Item>items){
            Console.WriteLine("showing items grouped by manufacturer and sorted amount");
            var result = items.OrderBy(i => i.amount).GroupBy(i => i.Manufacturer);
            foreach(var r in result){
                Console.WriteLine(r.ToString());
            }
        }
        static void Query10(List<Item>items,List<Manufacturer>manufacturers){
            Console.WriteLine("showing average price of third manufacturer's goods");
            var result = items.Where(i=>i.Manufacturer==3).Join(manufacturers,i=>i.Manufacturer,m=>m.id,(i,m)=>new{
                price=i.price,
                name=m.name
            });
            var sum=0;
            foreach(var r in result){
                sum += r.name=="Cheburashka"?r.price:0;
            }
            Console.WriteLine("Cheburashka's goods average price is {0}",sum/result.Count());
            
        }
        static void Query11(List<Item>items){
            Console.WriteLine("showing items, avg price of which is under average");
            int sum=0;
            foreach(var i in items){
                sum+=i.price;
            }
            var result=items.Where(i=>i.price<sum/items.Count());
            foreach(var r in result){
                Console.WriteLine(r.ToString());
            }
        }
        static void Query12(List<Manufacturer>manufacturers,List<Item>items){
            Console.WriteLine("showing avg price for goods of each manufacturer");
            foreach(var m in manufacturers){
                var result = items.Where(i=>i.Manufacturer==m.id);
                var sum=0;
                foreach(var r in result){
                    sum+=r.price;
                }
                Console.WriteLine("{0}'s goods avg price is:{1}",m.name,sum);
            }
        }
        static void Query13(List<Item>items){
            Console.WriteLine("showing cheapest and the least numerical items");
            var cheapest = items.Where(i=>i.price<100);
            var leastNumerical = items.Where(i=>i.amount<10);
            var result= cheapest.Concat(leastNumerical).Distinct();
            foreach(var r in result){
                    Console.WriteLine(r.ToString());
                }
        }
        static void Query14(List<Item>items,List<Manufacturer>manufacturers){
            Console.WriteLine("showing products names ordered by their names and their manufacturer's names");
            var result = from i in items
            join m in manufacturers on i.Manufacturer equals m.id
                orderby i.name,m.name
                select new{
                    name=i.name
                };
            foreach(var r in result){
                Console.WriteLine(r.name);
            }
        }
        static void Query15(List<Item>items,List<Deliveries>deliveries){
            Console.WriteLine("showing each item delivered 2/2/2021");
            var preResult = deliveries.Where(d=>d.date.Year==2021 && d.date.Month==2 && d.date.Day==2);
            foreach(var pr in preResult){
                var result = pr.items.Join(items,il=>il,i=>i.id,(il,i)=>new{
                    name=i.name
                });
                foreach(var r in result){
                    Console.WriteLine(r.name);
                }
            }
           
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public class Manufacturer{
            public int id{get;set;}
            public List<int>itemsId{get;set;}
            public string name{get;set;}

            public override string ToString()
            {
                string list = "";
                foreach(int i in itemsId){
                    list+=i+", ";
                }
                return string.Format("id:{0}, name:{1}, items:{2}",id,name,list);
            }
        }
        public class Deliveries{
            public int id;
            public DateOnly date;
            public List<int>items;
            public Deliveries(List<Item>list, DateOnly date, int deliveriesId){
                this.date=date;
                this.id=deliveriesId;
                this.items=new List<int>{};
                foreach (Item item in list){
                    foreach (int id in item.deliveriesId){
                        if (id==this.id){
                            this.items.Add(item.id);
                        }
                    }
                    
                }
            }
            public override string ToString()
            {
                string list = "";
                foreach(int i in items){
                    list+=i+", ";
                }
                return string.Format("id:{0}, date:{1}, items delivered:{2}",id,date,list);
            }

        }
        public class Item{
            public int id;
            public List<int> deliveriesId;
            public int Manufacturer;

            public int amount;
            public int price;
            public string name;

            public override string ToString()
            {
                string list = "";
                foreach(int i in deliveriesId){
                    list+=i+", ";
                }
                return string.Format("id:{0}, name:{1}, price:{2}, amount:{3}, deliveries:{4}, manufacturer:{5}",id,name,price,amount,list, Manufacturer);
            }
            
        }
        
        public class itemsDeliveries{
            public int itemId;
            public int deliveryId;
        }
    

    }
}