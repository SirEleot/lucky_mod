function getData(token, key){
    if(mp.storage.data[key] === undefined) 
        mp.events.callLocal("storage:responce:get", token, null);
    else  
        mp.events.callLocal("storage:responce:get", token, mp.storage.data[key]);
}
function setData(key, data){
    mp.storage.data[key] = data;
    mp.storage.flush();
}
function removeData(key){
    if(mp.storage.data[key] === undefined) return;
    delete mp.storage.data[key];    
    mp.storage.flush();
}

mp.events.add("storage:request:get", getData);
mp.events.add("storage:request:set", setData);
mp.events.add("storage:request:remove", removeData);