const express=require('express');
const router=express.Router();
const db=require('../db')



router.get('/getallorderbill',(req,res)=>{
    let query='call GetAllOrderBill()';
    db.query(query,function(error,results,fileds){
        res.json({result:results[0]});
    })
})

router.get('/getorderbillbyid/:idorderbill',(req,res)=>{
    let query='call GetOrderBill_ById(?)';
    db.query(query,[req.params.idorderbill],function(error,results,fileds){
        res.json({result:results[0]});
    })
})

router.post('/insert',(req,res)=>{
    let query='call Insert_Orderbill(?,?,?,?,?,?,?,?,?,?,?,?,?,?)';
    try{
        db.query(query,[
            req.body.IDOrderBill
            ,req.body.IDUser
            ,req.body.IDStore
            ,req.body.Date
            ,req.body.SubTotalPrice
            ,req.body.DeliveryPrice
            ,req.body.TotalPrice
            ,req.body.CustomerAddress
            ,req.body.CustomerPhone
            ,req.body.Note
            ,req.body.State
            ,req.body.Review
            ,req.body.StoreAnswer
            ,req.body.Rating],function(error,results,fields){
    
                if(error){
                    res.json({'result':'fail'});
                    return;
                }
                    
                res.json({'result':'ok'});
        })
    }
    catch(e){

    }
    finally{

    }
    
})


router.post('/deleteorderbillbyid/:idOrderBill',(req,res)=>{
    let query='delete from OrderBill where IDOrderBill=?';
    db.query(query,[req.params.idOrderBill],function(error,results,fields)
    {
        console.log('id la: ');
    })
    if(!error)
    res.send('đã xóa xong');
})

router.post('/update',(req,res)=>{
    let query='call OrderBill_Update(?,?,?,?,?,?,?,?,?,?,?,?,?,?)';
    let newquery='update OrderBill set  IDUser=?,IDStore=?,Date=?,SubTotalPrice=?,'+
    'DeliveryPrice=?,TotalPrice=?,CustomerAddress=?,CustomerPhone=?,Note=?,State=?,'+
    'Review=?,StoreAnswer=?,Rating=? where IDOrderBill=?';
    db.query(newquery,[req.body.IDUser
        ,req.body.IDStore
        ,req.body.Date
        ,req.body.SubTotalPrice
        ,req.body.DeliveryPrice
        ,req.body.TotalPrice
        ,req.body.CustomerAddress
        ,req.body.CustomerPhone
        ,req.body.Note
        ,req.body.State
        ,req.body.Review
        ,req.body.StoreAnswer
        ,req.body.Rating
        ,req.body.IDOrderBill
        ],function(error,results,fields){
                res.json("update đây");
                console.log(req.body);
        }
        );
})


module.exports=router;