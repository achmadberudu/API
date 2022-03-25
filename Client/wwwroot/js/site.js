// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const animals = [
    { name: "Fluffy", species: "cat", class: {name:"mamalia"}},
    { name: "Nemo", species: "fish", class: {name:"invertebrata"}},
    { name: "Garfield", species: "cat", class: {name:"mamalia"}},
    { name: "Dory", species: "fish", class: {name:"invertebrata"}},
    { name: "Camello", species: "cat", class: {name:"mamalia"}}
]
const kucing = [];
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "cat") {
        kucing.push(animals[i]);
    } else if (animals[i].species == "fish") {
        animals[i].class.name = "Non-Mamalia"
    }

}


console.log(kucing);
console.log(animals);
