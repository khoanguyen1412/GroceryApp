const express=require('express');
const app=express();
const bodyParser=require('body-parser');

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({
    extended: true
}));

const productRoutes=require('./routes/product');
const orderBillRoutes=require('./routes/orderbill');
const storeRoutes=require('./routes/store');
const userRoutes=require('./routes/user');
const productTypeRoutes=require('./routes/producttype');

app.get('/',(req,res)=>{
    res.send("hello");
})

app.use('/product',productRoutes);
app.use('/orderbill',orderBillRoutes);
app.use('/user',userRoutes);
app.use('/store',storeRoutes);
app.use('/producttype',productTypeRoutes);

module.exports=app;