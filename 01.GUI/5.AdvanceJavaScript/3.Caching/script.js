// Cache.open
caches
  .open("cache-1")
  .then((cache) =>
    cache
      .add("https://cdn.pixabay.com/photo/2015/04/19/08/32/rose-729509_640.jpg")
      .then(() => console.log("Image1 cached successfully"))
  );

// Cache.add
caches
  .open("cache-1")
  .then((cache) =>
    cache
      .add("./EARTHING GRID.jpg")
      .then(() => console.log("Image2 cached successfully"))
  );

// Cache.addAll
const urls = ["./EARTHING GRID.jpg", "./image.jpg"];
caches
  .open("cache-2")
  .then((cache) =>
    cache.addAll(urls).then(() => console.log("Images cached successfully"))
  );

caches.open("cache-2").then((cache) => {
  const request = new Request("./script.js");
  fetch(request).then((response) => {
    cache
      .put(request, response)
      .then(() => console.log("Script cached successfully"));
  });
});

// retrieving resources from the cache
const dataUrls = ["./data/data1.json", "./data/data2.json"];
caches
  .open("cache-3")
  .then((cache) =>
    cache.addAll(dataUrls).then(() => console.log("Data cached successfully"))
  );

//   Cache.match
caches.open("cache-3").then((cache) =>
  cache.match("./data/data1.json").then((response) => {
    if (response) {
      response.json().then((data) => console.log(data));
    } else {
      console.log("response not found");
    }
  })
);

// Cache.matchAll
caches.open("cache-3").then(cache => cache.matchAll().then(responseList => {
    console.log(responseList);
}));