import React,{useContext,createContext,useState ,PropsWithChildren}from 'react';

type AuthContextType={
    isAuthenticated:boolean;
    isCheckingAuth:boolean;
    setAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
    setIsCheckingAuth:React.Dispatch<React.SetStateAction<boolean>>;
}

type AuthProviderProps =PropsWithChildren;

const AuthContext = createContext <AuthContextType|undefined>(undefined);

const AuthProvider: AuthProviderProps =({children})=>{
const [isAuthenticated,setAuthenticated] =useState<boolean>(false)
    const [IsCheckingAuth,setIsCheckingAuth] =useState<boolean>(false)

    return (
        <AuthContext.Provider value={{isAuthenticated,setAuthenticated,isCheckingAuth,setIsCheckingAuth}}>
            {children}
        </AuthContext.Provider>
    )
}

export default function useAuth(){
    const context = useContext(AuthContext);
    if(!context){
        throw new Error("useAuth must be used within the context of AuthProvider")
    }
    return context;
}

export {AuthProvider};

