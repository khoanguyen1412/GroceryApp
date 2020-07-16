const express=require('express');
const router=express.Router();
const db=require('../db')

router.get('/',(req,res)=>{
    res.json({
        message:'hahahhahah'
    })
})
router.get('/abctest',(req,res)=>{
    db.query('SELECT * FROM urls', function (error ,results ,fields) {
        if (error) throw error;
        return res.send({ error: false, data: results, message: 'users list.' });
    });
})

router.post('/insert',(req,res)=>{
    let query='call insert_store(?,?)';
    db.query(query,[req.body.IDStore,req.body.Address],function(error,results,fields){
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

router.get('/getallstore',(req,res)=>{
    let query='call get_all_store()';
    db.query(query,function(error,results,fields){
        res.json({result:results[0]});
    })
    //res.json({message:"djaghfdf"})
})

router.get('/getstorebyid/:idstore',(req,res)=>{
    let query='call get_store_byid(?)';
    db.query(query,[req.params.idstore],function(error,results,fields){
        console.log('id la :'+req.params.idstore);
        res.json({result:results[0]});
    })
    //res.json({message:'id là '+req.params.idstore})
})

router.delete('/deletebyorderbill/:idorderbill/:state',(req,res)=>{
    res.json({message:'id là : '+req.params.idorderbill+' state là '+req.params.state})
})

router.post('/update',(req,res)=>{
    let query='call update_store(?,?,?,?,?,?)';
    let newquery='update store set	StoreName=?,ImageURL=?,StoreDescription=?,StoreAddress=?,RatingStore=?,IsActive=?    where IDStore=?';
    db.query(newquery,[req.body.StoreName,
    req.body.ImageURL,req.body.StoreDescription,req.body.StoreAddress,
    req.body.RatingStore,req.body.IsActive,req.body.IDStore],function(error,results,fields){

    })
    res.json({message: 'update đây'})
})


module.exports=router;