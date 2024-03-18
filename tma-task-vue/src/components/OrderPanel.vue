<template>
<div class="container">
    <h2> Order Request </h2>
    <div class="formContainer">
        <form id="addItemForm" @submit.prevent="handleSubmit">
            <label for="itemGroup">Item Group:</label><br>
            <select id="itemGroup" name="itemGroup" required v-model="itemGroup" @change="updateUnits()">
            <option value="" selected disabled hidden>Select Group</option>
            <optgroup label="ItemName">
                <option v-for="item in namesOptions" :key="item" :value="item">{{ item}}</option>
            </optgroup>
            </select>
            <label for="unitOfMeasurements" v-if="itemSelected">unitOfMeasurements:</label><br>
            <select id="unitOfMeasurements" name="unitOfMeasurements" required v-model="unitOfMeasurement" v-if="itemSelected" @change="updateQuantity()">
            <option value="" selected disabled hidden>Select Unit</option>
            <optgroup label="Unit">
                <option v-for="item in unitOfMeasurementOptions" :key="item" :value="item">{{ item}}</option>
            </optgroup>
            </select>
            <label for="quantity" v-if="unitSelected">Quantity:</label>
            <input type="number" id="quantity" name="quantity" required v-model="quantity" v-if="unitSelected">
            <label for="priceWithoutVAT" v-if="unitSelected">Price Without VAT:</label>
            <input type="number" step="0.01" id="priceWithoutVAT" name="priceWithoutVAT" required v-model="priceWithoutVat" v-if="unitSelected">
            <label for="comment" v-if="unitSelected">Comment:</label>
            <textarea id="comment" name="comment" v-model="comment" v-if="unitSelected"></textarea>
            <button type="submit" id="submit">Add Item</button>
            <button type="submit" id="addToList">Update Item</button>
            <button @click="cancelAdding"> Cancel </button>
        </form>
    </div>
</div>
</template>
<script>
import axios from 'axios';

export default {
    props: {
        givenItems : Array,
    },
    data() {
        return {
        unitOfMeasurementOptions: [],
        namesOptions: [],
        itemSelected : false,
        unitSelected : false,
        itemGroup: '',
        unitOfMeasurement: '',
        quantity: 0,
        priceWithoutVat : 0,
        comment: '',
        };
    },
    methods: {
        cancelAdding() {
            this.$emit("returnToList");
        },
        async handleSubmit(event) {
        event.preventDefault();
        if(event.submitter.id == "submit") {
            console.log("SUBMIT")

        }
        else if(event.submitter.id=="addToList") {
            console.log("ADDTOLIST")

        }
        },
        async sendAddRequest(requestBody) {
            axios.post('http://localhost:5171/Item/PostItem',requestBody)
            .then(response => {
                if(response)
                {
                    alert("Succesfully added to database");
                    this.cancelAdding();
                }
                else{
                    alert("Adding item failed");
                }
            })
            .catch(error => {
                alert('Error:', error);
            });
        },
        setGroupNames() {
            const names = []
            this.givenItems.forEach(item => {
                if(!names.includes(item.ItemGroup)) {
                    names.push(item.ItemGroup);
                }
            })
            return names;
        },
        setUnitOptions() {
            const units = []
            this.givenItems.forEach(item => {
                if(item.ItemGroup == this.itemGroup) {
                    if(!units.includes(item.UnitOfMeasurement)){
                        units.push(item.UnitOfMeasurement)
                    }
                }
            })
            return units;
        },
        updateUnits() {
            this.itemSelected = true;
            this.unitSelected = false;
            this.unitOfMeasurementOptions = this.setUnitOptions();

        },
        updateQuantity() {
          const foundItem = this.givenItems.find(item => {
            return item.ItemGroup == this.itemGroup && item.UnitOfMeasurement == this.unitOfMeasurement
          })
          if(foundItem) {
            this.quantity = foundItem.Quantity;
          }
          this.unitSelected = true;
        }
        },
    created() {
        this.namesOptions = this.setGroupNames();
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
    text-shadow: 0px 0px 10px #3ee8ff; /* Dodanie efektu cienia */
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