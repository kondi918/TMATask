<template>
<div class="container">
    <h2>Add Item Form</h2>
    <div class="formContainer">
        <form id="addItemForm" @submit.prevent="handleSubmit">
            <label for="itemGroup">Item Group:</label><br>
            <select id="itemGroup" name="itemGroup" required v-model="itemGroup">
            <option value="" selected disabled hidden>Select Group</option>
              <!--Fruits-->
              <optgroup label="Owoce">
                <option value="apple">Apples</option>
                <option value="banana">Bananas</option>
                <option value="orange">Oranges</option>
                <option value="pear">Pears</option>
                <option value="lettuce">Lettuces</option>
              </optgroup>
                <!--Vegetables-->
              <optgroup label="Warzywa">
                <option value="carrot">Carrots</option>
                <option value="potato">Potateos</option>
                <option value="tomato">Tomatoes</option>
                <option value="cucumber">Cucumbers</option>
                <option value="sałata">Salads</option>
              </optgroup>
            </select>            
        <label for="unitOfMeasurement">Unit Of Measurement:</label>
        <select id="unitOfMeasurement" name="unitOfMeasurement" required v-model="unitOfMeasurement">
        <option value="" selected disabled hidden>Select Unit</option>
        <option value="box">Boxes</option>
        <option value="kg">Kilograms</option>
        </select>
        <label for="quantity">Quantity:</label>
        <input type="number" id="quantity" name="quantity" required v-model="quantity">
        <label for="priceWithoutVAT">Price Without VAT:</label>
        <input type="number" step="0.01" id="priceWithoutVAT" name="priceWithoutVAT" required v-model="priceWithoutVat">
        <label for="status">Status:</label>
        <input type="text" id="status" name="status" required v-model="status">
        <label for="storageLocation">Storage Location:</label>
        <input type="text" id="storageLocation" name="storageLocation" v-model="storageLocation">
        <label for="contactPerson">Contact Person:</label>
        <textarea id="contactPerson" name="contactPerson" v-model="contactPerson"></textarea>
        <label for="photo">Photo:</label>
        <input type="file" id="photo" name="photo" accept="image/*" placeholder="Choose file" @change="checkImageSelected">
        <button type="submit">Add Item</button>
        <button @click="cancelAdding"> Cancel </button>
        </form>
    </div>
</div>
</template>
<script>
import ItemRequest from '@/Data/ItemRequest';
import axios from 'axios';

export default {
    data() {
        return {
        itemGroup: '',
        unitOfMeasurement: '',
        quantity: '',
        priceWithoutVat: '',
        status: '',
        storageLocation: '',
        contactPerson: '',
        photo: ''
        };
    },
    methods: {
        /*
        readFile(file){
            const reader = new FileReader();
            reader.onload = () => {
                const fileDataArray = new Uint8Array(reader.result);
                this.photo = fileDataArray;
            }
            reader.readAsArrayBuffer(file);
        },
        */
        checkImageSelected(event) {
        const file = event.target.files[0];
        if (file){
            if(!file.type.startsWith('image/')) {
                alert("You have to select image, other filles are not allowed");
                event.target.value = '';
            }else{
                const reader = new FileReader();
                 reader.onload = () => {
                this.photo = reader.result.split(',')[1]; // convert to base64
            };
                reader.readAsDataURL(file);
            }
            }
        },
        cancelAdding() {
            this.$emit("returnToList");
        },
        async handleSubmit(event) {
        event.preventDefault();
        var itemRequest = new ItemRequest(this.itemGroup,this.unitOfMeasurement,
        this.quantity,this.priceWithoutVat,this.status,this.storageLocation,this.contactPerson,this.photo)
        await this.sendRequest(itemRequest);
        },
        async sendRequest(requestBody) {
            console.log(requestBody);
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
        }
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
    height: calc(100%/20);
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