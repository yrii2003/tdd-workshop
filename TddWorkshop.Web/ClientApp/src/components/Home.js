import React, {useEffect, useState} from 'react';
import {AutoForm} from "uniforms-material";
import {JSONSchemaBridge} from 'uniforms-bridge-json-schema';


function adaptSchema(parameters){
    const schema = {
        title: 'Form',
        type: 'object',
        properties: {},
        required: []
    };
    
    const iKeys = Object.keys(parameters);
    for(let i = 0; i < iKeys.length; i++) {
        const jKeys = Object.keys(parameters[iKeys[i]].properties);
        for (let j = 0; j < jKeys.length; j++) {
            const key = iKeys[i] + '_' + jKeys[j];
            const type = parameters[iKeys[i]].properties[jKeys[j]].type ?? 'integer';
            
            schema.properties[key] = {
                type: type,
                id: key
            };
        }
    }

    return new JSONSchemaBridge(schema, _ => {});
}

const baseUrl = 'https://localhost:5001';
const swaggerUrl = baseUrl + '/swagger/v1/swagger.json';

function onSubmit(data, setResponse) {
    console.log(data);
    const json = {};
    const keys = Object.keys(data);
    for(let i = 0; i < keys.length; i++){
        const parts = keys[i].split('_');
        if(!json[parts[0]]){
            json[parts[0]] = {};
        }

        json[parts[0]][parts[1]] = data[keys[i]];
    }
    
    const params = {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: 'POST',
        body: JSON.stringify(json)
    };

    fetch(baseUrl + '/Calculator/Calculate', params)
        .then(response => response.json())
        .then(json => {
            setResponse(json);
        });
}

export function Home (){
    const [schema, setSchema] = useState(null);
    const [response, setResponse] = useState(null);
    
    useEffect(() => {
        fetch(swaggerUrl)
            .then(response => response.json())
            .then(json => {
                const personalInfo = json['components']['schemas']['PersonalInfo'];
                const creditInfo = json['components']['schemas']['CreditInfo'];
                const passportInfo = json['components']['schemas']['PassportInfo'];

                setSchema(adaptSchema({
                    personalInfo, creditInfo, passportInfo
                }));
            })
    }, []);
    
    return <div>
        {!schema 
            ? <div>Loading</div>
            : <AutoForm schema={schema} onSubmit={data => onSubmit(data, setResponse)}/>}
        {response != null 
            ? <div>
                <div>Is approved: {response.isApproved ? 'true' : 'false'}</div>
                {
                    response.isApproved
                        ?<div>Interest rate: <span id="interest-rate">{response.interestRate}</span></div>
                        : null}
            </div> 
            : null
        }
    </div>
}