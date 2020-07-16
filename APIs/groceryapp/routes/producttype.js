const express=require('express');
const router=express.Router();
const db=require('../db')


router.get('/getallproducttype',(req,res)=>{
    let query='call GetAllProductType()';
    db.query(query,function(error,results,fileds){
        res.json({result:results[0]});
    })
})

module.exports=router;