<template>
<div class="container">
    <h2 v-if="isAdding">Add Item Form</h2>
    <h2 v-if="!isAdding">Update Item Form</h2>
    <div class="formContainer">
        <form id="addItemForm" @submit.prevent="handleSubmit">
            <label for="itemGroup">Item Group:</label><br>
            <select id="itemGroup" name="itemGroup" required v-model="itemGroup">
            <option value="" selected disabled hidden>Select Group</option>
              <!--Fruits-->
              <optgroup label="Fruits">
                <option value="apple">Apples</option>
                <option value="banana">Bananas</option>
                <option value="orange">Oranges</option>
                <option value="pear">Pears</option>
              </optgroup>
                <!--Vegetables-->
              <optgroup label="Vegetables">
                <option value="carrot">Carrots</option>
                <option value="potato">Potateos</option>
                <option value="tomato">Tomatoes</option>
                <option value="cucumber">Cucumbers</option>
                <option value="lettuce">Lettuces</option>
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
        <button v-if=isAdding type="submit">Add Item</button>
        <button v-if=!isAdding type="submit">Update Item</button>
        <button @click="cancelAdding"> Cancel </button>
        </form>
    </div>
</div>
</template>
<script>
import ItemRequest from '@/Data/ItemRequest';
import ItemResponse from '@/Data/ItemResponse';
import axios from 'axios';

export default {
    props: {
        givenItem : Object,
        whatPanel: String
    },
    data() {
        return {
        itemGroup: '',
        unitOfMeasurement: '',
        quantity: '',
        priceWithoutVat: '',
        status: '',
        storageLocation: '',
        contactPerson: '',
        photo: '',
        isAdding : true
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
        if(this.isAdding) {
        var itemRequest = new ItemRequest(this.itemGroup,this.unitOfMeasurement,
        this.quantity,this.priceWithoutVat,this.status,this.storageLocation,this.contactPerson,this.photo)
        await this.sendAddRequest(itemRequest);
        }
        else {
            var itemUpdate = new ItemResponse(this.givenItem.ItemID,this.itemGroup,this.unitOfMeasurement,
        this.quantity,this.priceWithoutVat,this.status,this.storageLocation,this.contactPerson,this.photo)
        this.sendUpdateRequest(itemUpdate)
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
                alert("Error: " + error.response.data);
            });
        },
        async sendUpdateRequest(requestBody) {
            axios.post('http://localhost:5171/Item/UpdateItem',requestBody)
            .then(response => {
                if(response)
                {
                    alert("Succesfully updated item in database");
                    this.cancelAdding();
                }
                else{
                    alert("Updating item failed");
                }
            })
            .catch(error => {
                alert('Error:' + error.response.data);
            });
        },
        setObject() {
            this.itemGroup = this.givenItem.ItemGroup;
            this.unitOfMeasurement = this.givenItem.UnitOfMeasurement;
            this.quantity = this.givenItem.Quantity;
            this.priceWithoutVat = this.givenItem.PriceWithoutVAT;
            this.status = this.givenItem.Status;
            this.storageLocation = this.givenItem.StorageLocation;
            this.contactPerson = this.givenItem.ContactPerson;
            this.photo = this.givenItem.Photo;
            
        }
  },
  created() {
    if(this.whatPanel == "adding") {
        this.isAdding = true
    }
    else {
        this.isAdding = false;
        this.setObject()
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