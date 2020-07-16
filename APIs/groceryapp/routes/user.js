const express=require('express');
const router=express.Router();
const db=require('../db');

router.get('/',(req,res)=>{
    res.send("hahaha");
})

router.get('/getalluser',(req,res)=>{
    let query='call GetAllUser()';
    db.query(query,function(error,results,fileds){
        res.json({result:results[0]});
    })
})
router.get('/getuserbyid/:iduser',(req,res)=>{
    let query='call GetUserById(?)';
    db.query(query,[req.params.iduser],function(error,results,fileds){
        res.json({result:results[0]});
    })
})

router.post('/insert',(req,res)=>{

    let query='call Insert_User(?,?,?,?,?,?,?,?,?,?,?)';
    db.query(query,[req.body.IDUser,req.body.Password,req.body.IDStore,
    req.body.PhoneNumber,req.body.Address,req.body.Email,req.body.ImageURL,
    req.body.BirthDate,req.body.UserName,req.body.ExternalId,req.body.IsLogined],function(error,results,fields){
        if(error) throw error;
        else
        {
            res.json({
                message:'thêm thành công'
            })
        }
    })
    console.log(req.body);
})
router.post('/update',(req,res)=>{
    let query='call Update_User(?,?,?,?,?,?,?,?,?)';
    let new_query='update user set Password=?,IDStore=?,PhoneNumber=?,Address=?,Email=?,ImageURL=?,BirthDate=?,UserName=?,ExternalId=?,IsLogined=?  where IDUser=?';
    db.query(new_query,[req.body.Password,req.body.IDStore,
        req.body.PhoneNumber,req.body.Address,req.body.Email,req.body.ImageURL,req.body.BirthDate,req.body.UserName,req.body.ExternalId,req.body.IsLogined,req.body.IDUser],function(error,results,fields){

    })
    res.json({message: 'update đây'})
})


module.exports=router;