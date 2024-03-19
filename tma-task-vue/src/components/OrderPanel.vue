<template>
<div class="container">
    <h2> Order Request </h2>
    <div class="formContainer">
        <form id="addItemForm" @submit.prevent="handleSubmit">
            <label for="itemGroup">Item Group:</label><br>
            <select id="itemGroup" name="itemGroup" required v-model="itemGroup">
                <option :value="givenItem.ItemGroup">{{ givenItem.ItemGroup }}</option>
            </select>
            <label for="unitOfMeasurements">unitOfMeasurements:</label><br>
            <select id="unitOfMeasurements" name="unitOfMeasurements" required v-model="unitOfMeasurement">
                <option :value="givenItem.UnitOfMeasurement">{{ givenItem.UnitOfMeasurement }}</option>
            </select>
            <label for="quantity">Quantity:</label>
            <input type="number" id="quantity" name="quantity" required v-model="quantity">
            <label for="priceWithoutVAT">Price Without VAT:</label>
            <input type="number" step="0.01" id="priceWithoutVAT" name="priceWithoutVAT" required v-model="priceWithoutVat">
            <label for="comment">Comment:</label>
            <textarea id="comment" name="comment" v-model="comment"></textarea>
            <button type="submit" id="submit">Submit</button>
            <button type="submit" id="addToList">Add to Request</button>
            <button @click="cancelAdding"> Cancel </button>
        </form>
    </div>
</div>
</template>
<script>
import OrderRequest from '@/Data/OrderRequest'

export default {
    props: {
        givenItem : Array,
        employerName : String,
    },
    data() {
        return {
        itemGroup: '',
        unitOfMeasurement: '',
        quantity: 0,
        priceWithoutVat : 0,
        comment: '',
        };
    },
    methods: {
        resetForm() {
        this.itemGroup = '';
        this.unitOfMeasurement = '';
        this.quantity = 0;
        this.priceWithoutVat = '';
        this.comment = '';
        this.itemSelected = false;
        this.unitSelected = false;
        },
        cancelAdding() {
            this.$emit("returnToList");
        },
        submitRequest(order) {
            this.$emit("requestSubmit",order);
        },
        addSingleOrderToList(order) {
            this.$emit("addToOrderList",order)
        },
        getSingleOrder() {
            return new OrderRequest(this.givenItem.ItemID, this.itemGroup,this.unitOfMeasurement,this.quantity,this.priceWithoutVat,this.comment, this.employerName)
        },
        handleSubmit(event) {
        event.preventDefault();
        if(event.submitter.id == "submit") {
            this.submitRequest(this.getSingleOrder());
        }
        else if(event.submitter.id=="addToList") {
            this.addSingleOrderToList(this.getSingleOrder());
        }
        },
        },
    created() {
        this.itemGroup = this.givenItem.ItemGroup;
        this.unitOfMeasurement = this.givenItem.UnitOfMeasurement;
        this.quantity = this.givenItem.Quantity;
        this.priceWithoutVat = this.givenItem.PriceWithoutVAT;
    }
}
</script>
<style scoped>

.formContainer {
    display: flex;
    width: 60%;
    height: 90%;
    justify-content: center;
    align-items: center;
}
form {
    height: 100%;
    width: 100%;
    padding: 0px;
    margin: 0px;
}
h2 {
    color:white;
}
label {
    color:white;
}
input {
    width: 100%;
    height: calc(100%/20);
    padding: 0px;
    margin-top: 0.5%;
    margin-bottom:1%;
}
select {
    width: 100%;
    height: calc(100%/20);
    padding: 0px;
    margin-top: 0.5%;
    margin-bottom:1%;
}
textarea {
    width: 100%;
    height: calc(100%/10);
    padding: 0px;
    margin-top: 0.5%;
    margin-bottom:1%;
}
button {
    color: rgb(255, 255, 255);
    background-color: rgba(0, 0, 0, 0.11);
    border: solid rgb(105, 105, 105) 4px;
    text-decoration: none;
    text-shadow: 0px 0px 10px #3ee8ff; 
    font-size: large;
    font-weight: bolder; 
    cursor: pointer;
    margin-right: 2%;
}
button:hover {
    opacity:0.5;
}
@media screen and (max-width: 720px) {
    .formContainer {
        width: 80%;
    }
    form > * {
        margin-bottom: 3%;
    }   
}
</style>