namespace RestApiThothJson

#if FABLE_COMPILER
open Thoth.Json
#else
open Thoth.Json.Net
#endif

type ResponseGet = 
    {
        GetLinks : string
        Message : string
    } 

module ThothCoders =   

    //**************** GET ********************

    let internal encoderGet (result : ResponseGet) = 

        Encode.object
            [
                "GetLinks", Encode.string result.GetLinks  
                "Message", Encode.string result.Message    
            ]
    
    //**************** PUT ********************

    let internal encoderPut (result : string) = Encode.string result   
        