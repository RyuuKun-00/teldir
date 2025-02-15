


interface Props{
    src:string;
    alt:string;
}

export const HomeImage = ({src,alt}:Props)=>{
    return(
        <div className="technology">
                <img alt={alt} loading="lazy" style={{width:"100px"}} src={src}></img>
                <p className="textImage">{alt}</p>
        </div>
    )
}