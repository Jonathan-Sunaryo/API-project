

const animals = [
    { name: 'fluffy', species: 'cat', class: { name: 'mamalia' } },
    { name: 'Nemo', species: 'fish', class: { name: 'vertebrata' } },
    { name: 'hely', species: 'cat', class: { name: 'mamalia' } },
    { name: 'Dory', species: 'fish', class: { name: 'vertebrata' } },
    { name: 'ursa', species: 'cat', class: { name: 'mamalia' } }
]

const onlyCat = []

console.log(animals[2].class.name)

for (var i = 0; i < animals.length; i++)
{
    if (animals[i].species == 'cat')
    {
        onlyCat.push(animals[i])
    }

    if (animals[i].species == 'fish')
    {
        animals[i].class.name = 'Non-Mamalia'
    }
}

console.log('Hanya kucing')
for (var i = 0; i < onlyCat.length; i++) {
    console.log(onlyCat[i])
}

console.log('Diganti Non-Mamalia')

for (var i = 0; i < animals.length; i++) {
    console.log(animals[i])
}