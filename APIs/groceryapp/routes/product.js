const express=require('express');
const router=express.Router();
const db=require('../db')


router.post('/insert',(req,res)=>{
    let query='call insert_product(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)';
    db.query(query,[
        req.body.IDProduct
        ,req.body.IDType
        ,req.body.IDStore
        ,req.body.ProductName
        ,req.body.Unit
        ,req.body.ProductDescription
        ,req.body.QuantityOrder
        ,req.body.QuantityInventory
        ,req.body.Price
        ,req.body.ImageURL
        ,req.body.StateInStore
        ,req.body.IDOrderBill
        ,req.body.IDCart
        ,req.body.IDSourceProduct
        ,req.body.State],function(error,results,fields){
            if(error) throw error;
        else
        {
            res.json({
                message:'thêm thành công'
            })
        }
    })
    
})

router.get('/getallproduct',(req,res)=>{
    let query='call GetAllProduct()';
    db.query(query,function(error,results,fileds){
        res.json({result:results[0]});
    })
})

router.get('/getbyid/:idproduct',(req,res)=>{
    let query='call GetProduct_ById(?)';
    db.query(query,[req.params.idproduct],function(error,results,fileds){
        res.json({result:results[0]});
    })
})

router.post('/deletebyidorderbill/:idorderbill',(req,res)=>{
    let query='call Delete_ProductByOrderBill(?,?)';
    let sql='delete from product where IDOrderBill=? and State=3';
    db.query(sql,[req.params.idorderbill],function(error,results,fields){
        res.send("xoa product by idorderbill");
    })
})

router.post('/deleteproductbyid/:id',(req,res)=>{
    let query='delete from product where IDProduct=?';
    db.query(query,[req.params.id],function(error,results,fileds){
        res.send("xoa product byid");
    })
})


router.post('/update',(req,res)=>{
    let query='call Update_Product(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)';
    let newquery='update product set IDType=?,IDStore=?,ProductName=?,Unit=?,ProductDescription=?,QuantityOrder=?,QuantityInventory=?,Price=?,ImageURL=?,StateInStore=?,IDOrderBill=?,IDCart=?,IDSourceProduct=?,State=? where IDProduct=?';
    db.query(newquery,[req.body.IDType
        ,req.body.IDStore
        ,req.body.ProductName
        ,req.body.Unit
        ,req.body.ProductDescription
        ,req.body.QuantityOrder
        ,req.body.QuantityInventory
        ,req.body.Price
        ,req.body.ImageURL
        ,req.body.StateInStore
        ,req.body.IDOrderBill
        ,req.body.IDCart
        ,req.body.IDSourceProduct
        ,req.body.State
        ,req.body.IDProduct
    ],function(error,results,fields){
            res.send("product update");
            console.log(req.body);
    })
})


module.exports=router;