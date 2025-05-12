interface Props{
    name:string;
    number: string;
}


export const ContactTitle = ({name,number}:Props)=>{
    return (
        
        <div className="cardTitle">
            <p className="card__name">{name}</p>
            <p className="card__number">{number} </p>
        </div>

    );
}