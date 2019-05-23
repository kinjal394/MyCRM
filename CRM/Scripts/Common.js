function ParseFloat(objStr) {
    if (isNaN(parseFloat(objStr)))
        return 0;
    else
        return parseFloat(objStr);
}