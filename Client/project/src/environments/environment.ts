// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

const identityProtocolAndDomain: string = "http://localhost:7003";
const userProtocolAndDomain: string = "http://localhost:7003";
const postsProtocolAndDomain: string = "http://localhost:7003";

export const environment = {
  production: false,
  authTokenHeaderKey: "Authorization",
  apiRoutes: {
    identity: {
      signUp: userProtocolAndDomain + "/api/app/v1/user/signup",
      login: identityProtocolAndDomain + "/api/identity/v1/authentication/login",
      authorizeUser: identityProtocolAndDomain + "/api/identity/v1/authorize/user",
      informationUserById: identityProtocolAndDomain + "/api/identity/v1/information/authentication/user",
      informationUsersByNameOrEmail: identityProtocolAndDomain + "/api/identity/v1/information/authentication/users",
    },
    user: {
      userProfile: userProtocolAndDomain + "/api/app/v1/user/profile",
      biography: userProtocolAndDomain + "/api/app/v1/user/biography"
    },
    posts: {
      post: postsProtocolAndDomain + "/api/app/v1/post",
      homePage: postsProtocolAndDomain + "/api/app/v1/post/homepage",
      image: postsProtocolAndDomain + "/api/app/v1/post/image",
    }
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
