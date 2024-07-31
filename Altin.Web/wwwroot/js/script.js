let previousData = {
    gramAlis: null,
    gramSatis: null,
    gramYuzde: null,
    ceyrekAlis: null,
    ceyrekSatis: null,
    yarimAlis: null,
    yarimSatis: null,
    yirmiikialis: null,
    yirmiikisatis: null,
    tamalis: null,
    tamsatis: null,
    gremsealis: null,
    gremsesatis: null,
    ataalis: null,
    atasatis: null,
    kulcealis: null,
    kulcesatis: null,
    dolaralis: null,
    dolarsatis: null,
    euroalis: null,
    eurosatis: null,
    eurusdalis: null,
    eurusdsatis: null,
    xauusdalis: null,
    xauusdsatis: null,
};

function updateElement(id, newValue, previousValue) {
    const element = document.getElementById(id);
    element.innerText = newValue;

    if (previousValue !== null) {
        if (newValue > previousValue) {
            element.style.backgroundColor = 'green';
            element.style.color = 'white';
        } else if (newValue < previousValue) {
            element.style.backgroundColor = 'red';
            element.style.color = 'white';
        } else {
            element.style.color = 'black';
            element.style.backgroundColor = 'transparent';
        }
    }
}

function percentageChange(id, currentValue) {
    const element = document.getElementById(id);
    element.innerText = "%" + currentValue;
    
    iconElement = document.getElementById(id + "icon");

    if (currentValue > 0) {
        element.style.backgroundColor = 'green';
        element.style.color = 'white';
        
        iconElement.className = "float-start fas fa-sort-up ";
        
    } else if (currentValue < 0) {
        element.style.backgroundColor = 'red';
        element.style.color = 'white';

        element.className = "float-start fas fa-sort-down";
    } else {
        element.style.color = 'black';
        element.style.backgroundColor = 'transparent';
    }
}

async function fetchData() {
    const response = await fetch('https://maydagold.com/kurlar/webcanli.json');
    const data = await response.json();

    const newData = {
        gramAlis: data[0][0].alis,
        gramSatis: data[0][0].satis,
        gramYuzde: data[0][0].yuzde,
        ceyrekAlis: data[0][3].alis,
        ceyrekSatis: data[0][3].satis,
        yarimAlis: data[0][4].alis,
        yarimSatis: data[0][4].satis,
        yirmiikialis: data[0][1].alis,
        yirmiikisatis: data[0][1].satis,
        tamalis: data[0][5].alis,
        tamsatis: data[0][5].satis,
        gremsealis: data[0][6].alis,
        gremsesatis: data[0][6].satis,
        ataalis: data[0][7].alis,
        atasatis: data[0][7].satis,
        kulcealis: data[0][1].alis,
        kulcesatis: data[0][1].satis,
        dolaralis: data[1][2].alis,
        dolarsatis: data[1][2].satis,
        euroalis: data[1][3].alis,
        eurosatis: data[1][3].satis,
        eurusdalis: data[1][4].alis,
        eurusdsatis: data[1][4].satis,
        xauusdalis: data[1][1].alis,
        xauusdsatis: data[1][1].satis,
    };

    updateElement('gramalis', newData.gramAlis, previousData.gramAlis);
    updateElement('gramsatis', newData.gramSatis, previousData.gramSatis);
    percentageChange('gramyuzde',  newData.gramYuzde);
    updateElement('ceyrekalis', newData.ceyrekAlis, previousData.ceyrekAlis);
    updateElement('ceyreksatis', newData.ceyrekSatis, previousData.ceyrekSatis);
    updateElement('yarimalis', newData.yarimAlis, previousData.yarimAlis);
    updateElement('yarimsatis', newData.yarimSatis, previousData.yarimSatis);
    updateElement('yirmiikialis', newData.yirmiikialis, previousData.yirmiikialis);
    updateElement('yirmiikisatis', newData.yirmiikisatis, previousData.yirmiikisatis);
    updateElement('tamalis', newData.tamalis, previousData.tamalis);
    updateElement('tamsatis', newData.tamsatis, previousData.tamsatis);
    updateElement('gremsealis', newData.gremsealis, previousData.gremsealis);
    updateElement('gremsesatis', newData.gremsesatis, previousData.gremsesatis);
    updateElement('ataalis', newData.ataalis, previousData.ataalis);
    updateElement('atasatis', newData.atasatis, previousData.atasatis);
    updateElement('kulcealis', newData.kulcealis, previousData.kulcealis);
    updateElement('kulcesatis', newData.kulcesatis, previousData.kulcesatis);
    updateElement('dolaralis', newData.dolaralis, previousData.dolaralis);
    updateElement('dolarsatis', newData.dolarsatis, previousData.dolarsatis);
    updateElement('euroalis', newData.euroalis, previousData.euroalis);
    updateElement('eurosatis', newData.eurosatis, previousData.eurosatis);
    updateElement('eurusdalis', newData.eurusdalis, previousData.eurusdalis);
    updateElement('eurusdsatis', newData.eurusdsatis, previousData.eurusdsatis);
    updateElement('xauusdalis', newData.xauusdalis, previousData.xauusdalis);
    updateElement('xauusdsatis', newData.xauusdsatis, previousData.xauusdsatis);

    previousData = newData;
}

window.onload = () => {
    fetchData();
    setInterval(fetchData, 5000);
};
