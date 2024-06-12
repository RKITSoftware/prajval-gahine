export function createUserDs() {

    const userDs = new DevExpress.data.CustomStore({
        loadMode: "raw",
        load: function (options) {
            let deferred = $.Deferred();

            $.ajax({
                url: "http://localhost:5114/api/user",
                method: "GET",
                data: options.skip ? { skip: options.skip, take: options.take } : undefined,
                success: function (result) {
                    deferred.resolve(result);
                },
                error: function () {
                    deferred.reject("Data Loading Error");
                }
            });

            return deferred.promise();
        }
    });

    return new DevExpress.data.DataSource({
        store: userDs
    });
}