var mysql=require('mysql');

var dbCon=mysql.createPool({
  connectionLimit : 10,
  /*host: 'localhost',
  user: 'root',
  password: 'password',
  database: 'grocery',*/
   host: "datadidong.mysql.database.azure.com",
   user: "adminacc@datadidong",
   password: '@quenmk1',
   database: 'grocery',
   port: 3306,
   ssl:true,
  dateStrings: ['DATE','DATETIME']
});







 module.exports=dbCon;
