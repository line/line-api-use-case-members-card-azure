window.addEventListener('DOMContentLoaded', function(){
    let lang = getParam("lang");
    const defaultLang = "ja";
    const supportedLangList = ["ja"]
    let jsonPath = ''

    if(supportedLangList.indexOf(lang) >= 0){
        jsonPath = "lang_message/" + lang + ".json";
        localStorage.setItem('locale', lang);
    } else {
        lang = localStorage.getItem('locale');
        if(supportedLangList.indexOf(lang) >= 0){
            jsonPath = 'lang_message/'+ lang + '.json';
        }else {
            jsonPath = 'lang_message/'+ defaultLang + '.json';
        }
    }

    var glot = new Glottologist();
    glot.import(jsonPath).then(() => {
        glot.render();
    });
});

function getParam(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}