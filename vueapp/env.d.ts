/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly VITE_Base_Url: string;
    readonly VITE_Api_Url: string;
    // readonly VITE_AppInsights_InstrumentationKey: string;
    // readonly VITE_IpStackApiKey: string;
  }

  interface ImportMeta {
    readonly env: ImportMetaEnv;
  }
